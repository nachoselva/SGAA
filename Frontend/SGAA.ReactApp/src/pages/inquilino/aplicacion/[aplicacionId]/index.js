import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { getAplicacion } from '/src/api/inquilino';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { AplicacionLeerForm } from '/src/sections/aplicacion/aplicacion-leer-form';

const Page = () => {
  const router = useRouter();
  const [aplicacion, setAplicacion] = useState(null);
  const aplicacionId = router.query.aplicacionId;

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/inquilino/aplicacion', title: 'Aplicaciones' },
    { url: '/inquilino/aplicacion/' + aplicacionId, title: aplicacionId }
  ];

  useEffect(() => {
    getAplicacion(aplicacionId)
      .then((response) => {
        setAplicacion(response);
      });
  }, []);

  return (
    <FancyFormPage
      roles={['Inquilino']}
      form={aplicacion && <AplicacionLeerForm aplicacion={aplicacion} />}
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
