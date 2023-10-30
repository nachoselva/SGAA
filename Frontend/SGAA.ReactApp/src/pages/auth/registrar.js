import { Box, Link, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import NextLink from 'next/link';
import React, { useState } from 'react';
import { registrarUsuario } from '/src/api/auth';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';
import { UsuarioCrearForm } from '/src/sections/usuario/usuario-crear-form';

const Page = () => {
  const [confirmation, setConfirmation] = useState(false);

  return (
    <>
      <Head>
        <title>
          Registrar Usuario
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
              Registrar Usuario
            </Typography>
            <Typography
              color="text.secondary"
              variant="body2"
            >
              ¿Ya tiene una cuenta?
              &nbsp;
              <Link
                component={NextLink}
                href="/auth/login"
                underline="hover"
                variant="subtitle2"
              >
                Iniciar Sesión
              </Link>
            </Typography>
          </Stack>
          {!confirmation &&
            <UsuarioCrearForm handleSubmit={registrarUsuario} handleConfirmationChange={setConfirmation} />
          }
          {confirmation &&
            <>
              <p>Enviamos un correo de confirmación a su email. </p>
              <p>Por favor confirme el correo y ingrese al sistema mediante el link:&nbsp;
                <a href='auth/login'>Login</a></p>
            </>
          }
        </Box>
      </Box>
    </>
  );
};

Page.getLayout = (page) => (
  <AuthLayout>
    {page}
  </AuthLayout>
);

export default Page;
