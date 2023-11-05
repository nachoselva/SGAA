import {
  Card, Stack, Typography, Link
} from '@mui/material';
import Head from 'next/head';
import React from 'react';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';
import NextLink from 'next/link';

const Page = () => {

  return (
    <>
      <Head>
        <title>
          SGAA - Inicio
        </title>
      </Head>
      <Card sx={{ p: 2 }} sx={{
        border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
      }} >
        <div>
          <Stack
            spacing={1}
            sx={{ mb: 3 }}
          >
            <Typography variant="h4">
              Bienvenido
            </Typography>
            <Typography variant="h6">
              Seleccione una opción
            </Typography>
          </Stack>
          <Typography
            color="text.secondary"
            variant="body2"
          >
            Ingreso con usuario
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
          <Typography
            color="text.secondary"
            variant="body2"
          >
            Ingreso público
            &nbsp;
            <Link
              component={NextLink}
              href="/inicio"
              underline="hover"
              variant="subtitle2"
            >
              Ingresar
            </Link>
          </Typography>
          <Typography
            color="text.secondary"
            variant="body2"
          >
            Crear una cuenta
            &nbsp;
            <Link
              component={NextLink}
              href="/auth/registrar"
              underline="hover"
              variant="subtitle2"
            >
              Registrar usuario
            </Link>
          </Typography>
          <Typography
            color="text.secondary"
            variant="body2"
          >
            Olvidé mi contraseña
            &nbsp;
            <Link
              component={NextLink}
              href="/auth/recuperar-password"
              underline="hover"
              variant="subtitle2"
            >
              Recuperar contraseña
            </Link>
          </Typography>
        </div>
      </Card>
    </>
  );
};

Page.getLayout = (page) => (
  <AuthLayout>
    {page}
  </AuthLayout>
);

export default Page;
