import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getPostulacion } from '/src/api/inquilino';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PostulacionLeerForm } from '/src/sections/postulacion/postulacion-leer-form';

const Page = () => {
  const router = useRouter();
  const [postulacion, setPostulacion] = useState(null);
  const postulacionId = router.query.postulacionId;

  const getBreadcrumbsConfig = (postulacionId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: null, title: 'Postulaciones' },
      { url: '/inquilino/postulacion/' + postulacionId, title: postulacionId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(postulacionId);

  useEffect(() => {
    if (router.isReady)
      getPostulacion(postulacionId)
        .then((response) => {
          setPostulacion(response);
        });
  }, [router.isReady]);

  return (
    <FancyFormPage
      roles={['Inquilino']}
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
