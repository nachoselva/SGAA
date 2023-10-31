import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '../../../../components/fancy-form-page';
import { getPostulacion } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PostulacionLeerForm } from '/src/sections/postulacion/postulacion-leer-form';

const Page = () => {
  const router = useRouter();
  const [postulacion, setPostulacion] = useState(null);
  const postulacionId = router.query.postulacionId;

  const getBreadcrumbsConfig = (postulacionId) =>
    [
      { url: '/', title: 'Inicio' },
      { url: '/administrador/postulacion', title: 'Postulaciones' },
      { url: '/administrador/postulacion/' + postulacionId, title: postulacionId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(postulacionId);

  useEffect(() => {
    getPostulacion(postulacionId)
      .then((response) => {
        setPostulacion(response);
      });
  }, []);

  return (
    <FancyFormPage
      form={postulacion && <PostulacionLeerForm postulacion={postulacion} />}
      entityName={'Postulacion'}
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
