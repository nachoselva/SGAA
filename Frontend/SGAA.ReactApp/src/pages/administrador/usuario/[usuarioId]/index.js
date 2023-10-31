import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { FancyFormPage } from '../../../../components/fancy-form-page';
import { getUsuario } from '/src/api/administrador';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UsuarioLeerForm } from '/src/sections/usuario/usuario-leer-form';

const Page = () => {
  const router = useRouter();
  const [usuario, setUsuario] = useState(null);
  const usuarioId = router.query.usuarioId;

  const getBreadcrumbsConfig = (usuarioId) =>
    [
      { url: '/', title: 'Inicio' },
      { url: '/administrador/usuario', title: 'Usuarios' },
      { url: '/administrador/usuario/' + usuarioId, title: usuarioId }
    ];

  const breadcrumbsConfig = getBreadcrumbsConfig(usuarioId);

  useEffect(() => {
    getUsuario(usuarioId)
      .then((response) => {
        setUsuario(response);
      });
  }, []);

  return (
    <FancyFormPage
      form={usuario && <UsuarioLeerForm usuario={usuario} />}
      entityName={'Usuario'}
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
