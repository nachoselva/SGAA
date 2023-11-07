import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { actualizarAplicacion, getAplicacion } from '/src/api/inquilino';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { AplicacionCrearForm } from '/src/sections/aplicacion/aplicacion-crear-form';

const Page = () => {

  const router = useRouter();
  const [aplicacion, setAplicacion] = useState(null);
  const aplicacionId = router.query.aplicacionId;

  const onAplicacionUpdated = (result) => {
    if (result)
      router.push('/inquilino/aplicacion');
  };

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/inquilino/aplicacion', title: 'Aplicaciones' },
    { url: '/inquilino/aplicacion/editar', title: aplicacionId }
  ];

  useEffect(() => {
    if (router.isReady)
      getAplicacion(aplicacionId)
        .then((response) => {
          setAplicacion(response);
        });
  }, [router.isReady]);

  return (
    <FancyFormPage
      roles={['Inquilino']}
      form={aplicacion && <AplicacionCrearForm aplicacion={aplicacion} handleSubmit={actualizarAplicacion} handleConfirmationChange={onAplicacionUpdated} />}
      title={'Editar Aplicacion'}
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
