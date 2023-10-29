import { Box, Button, Link, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import Head from 'next/head';
import NextLink from 'next/link';
import React, { useState } from 'react';
import * as Yup from 'yup';
import { registrar } from '/src/api/auth';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';

const Page = () => {
  const [confirmation, setConfirmation] = useState(false);

  const formik = useFormik({
    initialValues: {
      email: '',
      nombre: '',
      apellido: '',
      password: '',
      rol: 'Inquilino',
      submit: null
    },
    validationSchema: Yup.object({
      email: Yup
        .string()
        .email('Formato de email inválido')
        .max(255)
        .required('Email es obligatorio'),
      nombre: Yup
        .string()
        .max(255)
        .required('Nombre es obligatorio'),
      apellido: Yup
        .string()
        .max(255)
        .required('Apellido es obligatorio'),
      password: Yup
        .string()
        .max(255)
        .required('Password es obligatorio'),
      rol: Yup
        .string()
        .max(20)
        .required('Rol es obligatorio')
    }),
    onSubmit: (values, helpers) => {
      registrar(values.email, values.nombre, values.apellido, values.password, values.rol)
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
          <div>
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
              <form
                noValidate
                onSubmit={formik.handleSubmit}
              >
                <Stack spacing={3}>
                  <TextField
                    error={!!(formik.touched.nombre && formik.errors.nombre)}
                    fullWidth
                    helperText={formik.touched.nombre && formik.errors.nombre}
                    label="Nombre"
                    name="nombre"
                    onBlur={formik.handleBlur}
                    onChange={formik.handleChange}
                    value={formik.values.nombre}
                  />
                  <TextField
                    error={!!(formik.touched.apellido && formik.errors.apellido)}
                    fullWidth
                    helperText={formik.touched.apellido && formik.errors.apellido}
                    label="Apellido"
                    name="apellido"
                    onBlur={formik.handleBlur}
                    onChange={formik.handleChange}
                    value={formik.values.apellido}
                  />
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
                  <TextField
                    fullWidth
                    label="Propietario / Inquilino"
                    name="rol"
                    onChange={formik.handleChange}
                    select
                    SelectProps={{ native: true }}
                    value={formik.values.rol}
                  >
                    <option key={'Inquilino'} value={'Inquilino'}>Inquilino</option>
                    <option key={'Propietario'} value={'Propietario'}>Propietario</option>
                  </TextField>
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
                <p>Enviamos un correo de confirmación a su email. </p>
                <p>Por favor confirme el correo y ingrese al sistema mediante el link:&nbsp;
                  <a href='auth/login'>Login</a></p>
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
