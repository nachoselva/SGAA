import { Card, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import { confirmarCorreo } from '/src/api/auth';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';

const Page = () => {
  const [confirmation, setConfirmation] = useState(false);
  const router = useRouter();
  const email = router.query.email;
  const token = router.query.token;

  useEffect(() => {
    if (router.isReady)
      if (email && token)
        confirmarCorreo(email, token)
          .then(() => {
            setConfirmation(true);
          });
  }, [router.isReady]);

  return (
    <>
      <Head>
        <title>
          SGAA - Confirmar Correo Electrónico
        </title>
      </Head>
      <Card sx={{ p: 2 }} sx={{
        border: 1, borderRadius: '8px', 'border- style': 'solid', 'border-width': '1px', 'border-color': '#1C2536', p: 2, mt: 1
      }} >
        <Stack
          spacing={1}
          sx={{ mb: 3 }}
        >
          <Typography variant="h4">
            Confirmar Correo
          </Typography>
        </Stack>
        {confirmation &&
          <>
            <p>Su correo fue confirmado. </p>
            <p>Por favor ingrese al sistema mediante el link:&nbsp;
              <a href='/auth/login'>Login</a></p>
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
