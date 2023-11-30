import { Button, Card, Link, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import Head from 'next/head';
import NextLink from 'next/link';
import React, { useState } from 'react';
import * as Yup from 'yup';
import { recuperarPassword } from '/src/api/auth';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';

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
          SGAA - Recuperar Contraseña
        </title>
      </Head>
      <Card sx={{ border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1 }} >
        <Stack
          spacing={1}
          sx={{ mb: 3 }}
        >
          <Typography variant="h4">
            Resetear Contraseña
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
        </Stack>
        <div>
          {!confirmation &&
            <form
              noValidate
              onSubmit={formik.handleSubmit}
            >
              <Stack spacing={3}>
                <TextField
                  variant="filled"
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
