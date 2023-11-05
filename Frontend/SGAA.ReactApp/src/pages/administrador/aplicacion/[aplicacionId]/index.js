import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { getAplicacion } from '/src/api/administrador';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { AplicacionLeerForm } from '/src/sections/aplicacion/aplicacion-leer-form';

const Page = () => {
  const router = useRouter();
  const [aplicacion, setAplicacion] = useState(null);
  const aplicacionId = router.query.aplicacionId;

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/administrador/aplicacion', title: 'Aplicaciones' },
    { url: '/administrador/aplicacion/' + aplicacionId, title: aplicacionId }
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
      roles={['Administrador']}
      form={aplicacion && <AplicacionLeerForm rol={'Administrador'} aplicacion={aplicacion} />}
      title={'Detalle Aplicacion'}
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
