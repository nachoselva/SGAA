import { useRouter } from 'next/router';
import { actualizarUnidad, getUnidad } from '/src/api/propietario';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UnidadLeerForm } from '/src/sections/unidad/unidad-leer-form';
import React, { useEffect, useState } from 'react';

const Page = () => {
  const router = useRouter();
  const [unidad, setUnidad] = useState(null);
  const unidadId = router.query.unidadId;

  const onUnidadCreated = (result) => {
    if (result)
      router.push('/propietario/unidad');
  };

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/propietario/unidad', title: 'Unidades' },
    { url: '/propietario/unidad/' + unidadId, title: unidadId }
  ];

  useEffect(() => {
    if (router.isReady)
    getUnidad(unidadId)
      .then((response) => {
        setUnidad(response);
      });
  }, [router.isReady]);

  return (
    <FancyFormPage
      roles={['Propietario']}
      form={unidad && <UnidadLeerForm unidad={unidad} handleSubmit={actualizarUnidad} handleConfirmationChange={onUnidadCreated} />}
      title={'Editar Unidad'}
      breadcrumbsConfig={breadcrumbsConfig}
    >
    </FancyFormPage>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
