import { Box, Button, Link, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import Head from 'next/head';
import React, { useState } from 'react';
import * as Yup from 'yup';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';
import { recuperarPassword } from '/src/api/auth';

const Page = () => {
  const [confirmation, setConfirmation] = useState(false);

  const formik = useFormik({
    initialValues: {
      email: '',
      submit: null
    },
    validationSchema: Yup.object({
      email: Yup
        .string()
        .email('Formato de email inválido')
        .max(255)
        .required('Email es obligatorio')
    }),
    onSubmit: (values, helpers) => {
      recuperarPassword(values.email)
      .then(() => {
        setConfirmation(true);
      })
      .catch((err) => {
        if (err.statusCode == 400) {
          helpers.setStatus({ success: false });
          helpers.setErrors(err.body)
          helpers.setSubmitting(false);
        } else {
          throw err;
        }
      });
    }
  });

  return (
    <>
      <Head>
        <title>
          Recuperar Contraseña
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
              Resetear Contraseña
            </Typography>
          </Stack>
          <div>
            {!confirmation &&
              <form
                noValidate
                onSubmit={formik.handleSubmit}
              >
                <Stack spacing={3}>
                  <TextField
                    error={!!(formik.touched.email && formik.errors.email)}
                    fullWidth
                    helperText={formik.touched.email && formik.errors.email}
                    label="Email"
                    name="email"
                    onBlur={formik.handleBlur}
                    onChange={formik.handleChange}
                    type="email"
                    value={formik.values.email}
                  />
                </Stack>
                {formik.errors.submit && (
                  <Typography
                    color="error"
                    sx={{ mt: 3 }}
                    variant="body2"
                  >
                    {formik.errors.submit}
                  </Typography>
                )}
                <Button
                  fullWidth
                  size="large"
                  sx={{ mt: 3 }}
                  type="submit"
                  variant="contained"
                >
                  Registrar
                </Button>
              </form>
            }
            {confirmation &&
              <>
                <p>Enviamos un correo de recuperación a su email. </p>
                <p>Por favor siga las instruciones.</p>
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
