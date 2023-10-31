import { Box, Stack, Typography } from '@mui/material';
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
    if (email && token)
      confirmarCorreo(email, token)
        .then(() => {
          setConfirmation(true);
        });
  }, [email, token]);

  return (
    <>
      <Head>
        <title>
          Confirmar Correo Electrónico
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
              Confirmar Correo
            </Typography>
          </Stack>
          <div>
            {confirmation &&
              <>
                <p>Su correo fue confirmado. </p>
                <p>Por favor ingrese al sistema mediante el link:&nbsp;
                  <a href='/auth/login'>Login</a></p>
              </>
            }
          </div>
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
