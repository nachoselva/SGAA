import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getPublicacion } from '/src/api/inquilino';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PublicacionLeerForm } from '/src/sections/publicacion/publicacion-leer-form';

const Page = () => {
  const router = useRouter();
  const [publicacion, setPublicacion] = useState(null);
  const publicacionId = router.query.publicacionId;

  const getBreadcrumbsConfig = (publicacionId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: null, title: 'Publicaciones' },
      { url: '/inquilino/publicacion/' + publicacionId, title: publicacionId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(publicacionId);

  useEffect(() => {
    if (router.isReady)
      getPublicacion(publicacionId)
        .then((response) => {
          setPublicacion(response);
        });
  }, [router.isReady]);

  return (
    <FancyFormPage
      roles={['Inquilino']}
      form={publicacion && <PublicacionLeerForm publicacion={publicacion} />}
      title={'Detalle Publicación'}
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
