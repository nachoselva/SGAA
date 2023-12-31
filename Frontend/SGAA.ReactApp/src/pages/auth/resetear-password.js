import { Button, Card, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import Head from 'next/head';
import React, { useState } from 'react';
import * as Yup from 'yup';
import { resetearPassword } from '/src/api/auth';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';
import { useRouter } from 'next/router';

const Page = () => {
  const router = useRouter();
  const [confirmation, setConfirmation] = useState(false);

  const token = router.query.token;
  const email = router.query.email;

  const formik = useFormik({
    initialValues: {
      password: '',
      submit: null
    },
    validationSchema: Yup.object({
      password: Yup
        .string()
        .max(255)
        .required('Contraseña es obligatorio')
    }),
    onSubmit: (values, helpers) => {
      resetearPassword(email, token, values.password)
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
          SGAA - Resetear Contraseña
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
                Resetear
              </Button>
            </form>
          }
          {confirmation &&
            <>
              <p>La contraseña fue reseteada con éxito. </p>
              <p>Por favor ingrese al sistema mediante el link:&nbsp;
                <a href='auth/login'>Login</a></p>
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
