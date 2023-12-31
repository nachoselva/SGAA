import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { getUsuario } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UsuarioLeerForm } from '/src/sections/usuario/usuario-leer-form';

const Page = () => {
  const router = useRouter();
  const [usuario, setUsuario] = useState(null);
  const usuarioId = router.query.usuarioId;

  const getBreadcrumbsConfig = (usuarioId) =>
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/administrador/usuario', title: 'Usuarios' },
      { url: '/administrador/usuario/' + usuarioId, title: usuarioId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(usuarioId);

  useEffect(() => {
    if (router.isReady)
      getUsuario(usuarioId)
        .then((response) => {
          setUsuario(response);
        });
  }, [router.isReady]);

  return (
    <FancyFormPage
      roles={['Administrador']}
      form={usuario && <UsuarioLeerForm usuario={usuario} />}
      title={'Detalle Usuario'}
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
