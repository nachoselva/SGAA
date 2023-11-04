import { useRouter } from 'next/router';
import React, { useEffect, useRef, useState } from 'react';
import { editarCurrentUsuario, getCurrentUsuario } from '/src/api/auth';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UsuarioEditarForm } from '/src/sections/usuario/usuario-editar-form';

const Page = () => {
  const router = useRouter();
  const [usuario, setUsuario] = useState(null);
  const initialized = useRef(false);

  const onConfirmation = (result) => {
    if (result) {
      router.push('/');
    }
  }

  useEffect(() => {
    if (!initialized.current) {
      initialized.current = true;
      getCurrentUsuario()
        .then((response) => {
          setUsuario(response);
        });
    }
  }, []);

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/auth/editar', title: 'Editar Usuario' },
  ];

  return (
    <FancyFormPage
      form={usuario && <UsuarioEditarForm usuario={usuario} handleSubmit={editarCurrentUsuario} handleConfirmationChange={onConfirmation}></UsuarioEditarForm>}
      title={'Editar Usuario'}
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
