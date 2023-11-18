import {
    Button, Card, Link,
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
        .email('Email inválido')
        .max(255)
        .required('Email es obligatorio'),
      password: Yup
        .string()
        .max(255)
        .required('Contraseña es obligatorio')
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
          SGAA - Login
        </title>
      </Head>
      <Card sx={{ p: 2 }} sx={{
        border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
      }} >

        <Stack
          spacing={1}
          sx={{ mb: 3 }}
        >
          <Typography variant="h4">
            Ingresar
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
        </Stack>
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
            <TextField
              variant="filled"
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
