import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getPublicacion } from '/src/api/common';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PublicacionActivaForm } from '/src/sections/publicacion/publicacion-activa-form';

const Page = () => {
  const router = useRouter();
  const [publicacion, setPublicacion] = useState(null);
  const codigo = router.query.codigo;

  const getBreadcrumbsConfig = (codigo) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/publicacion', title: 'Publicaciones' },
      { url: '/publicacion/' + codigo, title: "Publicación" }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(codigo);

  useEffect(() => {
    if (router.isReady)
      getPublicacion(codigo)
        .then((response) => {
          setPublicacion(response);
        });
  }, [router.isReady]);

  return (
    <>
      <FancyFormPage
        form={publicacion && <PublicacionActivaForm publicacion={publicacion} />}
        title={'Detalle Publicación'}
        breadcrumbsConfig={breadcrumbsConfig}
        allowAnnonymous={true}
      />

    </>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
