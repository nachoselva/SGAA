import { Card, Link, Stack, Typography } from '@mui/material';
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
          SGAA - Registrar Usuario
        </title>
      </Head>
      <Card sx={{ border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1 }} >
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
