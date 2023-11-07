import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getPostulacion } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PostulacionLeerForm } from '/src/sections/postulacion/postulacion-leer-form';

const Page = () => {
  const router = useRouter();
  const [postulacion, setPostulacion] = useState(null);
  const postulacionId = router.query.postulacionId;

  const getBreadcrumbsConfig = (postulacionId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/administrador/postulacion', title: 'Postulaciones' },
      { url: '/administrador/postulacion/' + postulacionId, title: postulacionId }
    ];

  useEffect(() => {
    if (router.isReady)
      getAplicacion(aplicacionId)
        .then((response) => {
          setAplicacion(response);
        });
  }, [router.isReady]);

  const breadcrumbsConfig = getBreadcrumbsConfig(postulacionId);

  useEffect(() => {
    getPostulacion(postulacionId)
      .then((response) => {
        setPostulacion(response);
      });
  }, []);

  return (
    <FancyFormPage
      roles={['Administrador']}
      form={postulacion && <PostulacionLeerForm postulacion={postulacion} />}
      title={'Detalle Postulacion'}
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
