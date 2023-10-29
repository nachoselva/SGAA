import { Box, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import React, { useState, useEffect } from 'react';
import { editarCurrentUsuario, getCurrentUsuario } from '/src/api/auth';
import { AuthGuard } from '/src/guards/auth-guard';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';
import { UsuarioEditarForm } from '/src/sections/usuario/usuario-editar-form';
import { useRouter } from 'next/router';

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

  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          Editar Usuario
        </title>
      </Head>
      <Box
        sx={{
          flex: '1 1 auto',
          alignItems: 'center',
          display: 'flex',
          justifyContent: 'center'
        }}
      >
        <Box
          sx={{
            maxWidth: 550,
            px: 3,
            py: '100px',
            width: '100%'
          }}
        >
          <Stack
            spacing={1}
            sx={{ mb: 3 }}
          >
            <Typography variant="h4">
              Editar Usuario
            </Typography>
          </Stack>
          {usuario &&
            <UsuarioEditarForm nombre={usuario.nombre} apellido={usuario.apellido} email={usuario.email} rol={usuario.roles} handleSubmit={editarCurrentUsuario} handleConfirmationChange={onConfirmation}></UsuarioEditarForm>
          }
        </Box>
      </Box>
    </AuthGuard>
  );
};

Page.getLayout = (page) => (
  <AuthLayout>
    {page}
  </AuthLayout>
);

export default Page;
