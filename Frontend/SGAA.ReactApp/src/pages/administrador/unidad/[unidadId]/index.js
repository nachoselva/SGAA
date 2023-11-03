import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getUnidad } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UnidadLeerForm } from '/src/sections/unidad/unidad-leer-form';

const Page = () => {
  const router = useRouter();
  const [unidad, setUnidad] = useState(null);
  const unidadId = router.query.unidadId;

  const getBreadcrumbsConfig = (unidadId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/administrador/unidad', title: 'Unidades' },
      { url: '/administrador/unidad/' + unidadId, title: unidadId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(unidadId);

  useEffect(() => {
    getUnidad(unidadId)
      .then((response) => {
        setUnidad(response);
      });
  }, []);

  return (
    <FancyFormPage
      roles={['Administrador']}
      form={unidad && <UnidadLeerForm unidad={unidad} />}
      title={'Detalle Unidad'}
      breadcrumbsConfig={breadcrumbsConfig}>
    </FancyFormPage>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
