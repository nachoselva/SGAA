import { Box, Breadcrumbs, Card, Container, Grid, Link, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { editarCurrentUsuario, getCurrentUsuario } from '/src/api/auth';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UsuarioEditarForm } from '/src/sections/usuario/usuario-editar-form';
import { FancyFormPage } from '/src/components/fancy-form-page';

const Page = () => {
  const router = useRouter();
  const [usuario, setUsuario] = useState(null);

  const onConfirmation = (result) => {
    if (result) {
      router.push('/');
    }
  }

  useEffect(() => {
    getCurrentUsuario()
      .then((response) => {
        setUsuario(response);
      });
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
