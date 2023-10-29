import { Box, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import React, { useState, useEffect } from 'react';
import { confirmarCorreo } from '/src/api/confirmar-correo';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';

const Page = () => {
  const [confirmation, setConfirmation] = useState(false);
  const [errorMessage, setErrorMessage] = useState(false);

  useEffect(() =>
  {
    async function fn() {
      const searchParams = new URLSearchParams(document.location.search)
      const token = searchParams.get('token');
      console.log(token);
      const email = searchParams.get('email');
      const response = await confirmarCorreo(email, token);

      if (response.status == 200) {
        setConfirmation(true);
      }
      else if (response.status == 400) {
        setErrorMessage("Ocurrió un error durante la confirmación del correo electrónico");
      }
      else {
        throw new Error(response);
      }
    }

    fn();
  }, []);

  return (
    <>
      <Head>
        <title>
          Resetear Contraseña
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
                  <a href='auth/login'>Login</a></p>
              </>
            }
            {errorMessage &&
              <p> {errorMessage} </p>
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
