import {
    Box,
    Button, Link,
    Stack, TextField,
    Typography
} from '@mui/material';
import { useFormik } from 'formik';
import Head from 'next/head';
import NextLink from 'next/link';
import React from 'react';
import * as Yup from 'yup';
import { useAuth } from '/src/hooks/use-auth';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';

const Page = () => {
  const auth = useAuth();
  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
      submit: null
    },
    validationSchema: Yup.object({
      email: Yup
        .string()
        .email('Must be a valid email')
        .max(255)
        .required('Email is required'),
      password: Yup
        .string()
        .max(255)
        .required('Password is required')
    }),
    onSubmit: (values, helpers) =>
      auth.signIn(values.email, values.password)
        .catch((err) => {
          if (err.statusCode == 401) {
            helpers.setStatus({ success: false });
            helpers.setErrors({ submit: "Usuario o contraseña incorrectos" });
            helpers.setSubmitting(false);
            return;
          }
          else {
            throw err;
          }
        })
  });

  return (
    <>
      <Head>
        <title>
          Login
        </title>
      </Head>
      <Box
        sx={{
          backgroundColor: 'background.paper',
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
          <div>
            <Stack
              spacing={1}
              sx={{ mb: 3 }}
            >
              <Typography variant="h4">
                Sistema de Gestión de Alquileres Autónomos
              </Typography>
              <Typography
                color="text.secondary"
                variant="body2"
              >
                ¿No tiene una cuenta?
                &nbsp;
                <Link
                  component={NextLink}
                  href="/auth/registrar"
                  underline="hover"
                  variant="subtitle2"
                >
                  Registrarse
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
                  Recuperar password
                </Link>
              </Typography>
            </Stack>
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
                <TextField
                  error={!!(formik.touched.password && formik.errors.password)}
                  fullWidth
                  helperText={formik.touched.password && formik.errors.password}
                  label="Contraseña"
                  name="password"
                  onBlur={formik.handleBlur}
                  onChange={formik.handleChange}
                  type="password"
                  value={formik.values.password}
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
                Login
              </Button>
            </form>
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
