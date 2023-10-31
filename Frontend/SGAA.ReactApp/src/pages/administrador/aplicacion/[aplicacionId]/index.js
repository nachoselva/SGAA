import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '../../../../components/fancy-form-page';
import { getAplicacion } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { AplicacionLeerForm } from '/src/sections/aplicacion/aplicacion-leer-form';

const Page = () => {
  const router = useRouter();
  const [aplicacion, setAplicacion] = useState(null);
  const aplicacionId = router.query.aplicacionId;

  const getBreadcrumbsConfig = (aplicacionId) =>
    [
      { url: '/', title: 'Inicio' },
      { url: '/administrador/aplicacion', title: 'Aplicaciones' },
      { url: '/administrador/aplicacion/' + aplicacionId, title: aplicacionId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(aplicacionId);

  useEffect(() => {
    getAplicacion(aplicacionId)
      .then((response) => {
        setAplicacion(response);
      });
  }, []);

  return (
    <FancyFormPage
      form={aplicacion && <AplicacionLeerForm aplicacion={aplicacion} />}
      entityName={'Aplicacion'}
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
