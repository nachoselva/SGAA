import { Box, Button, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import Head from 'next/head';
import React, { useState } from 'react';
import * as Yup from 'yup';
import { Layout as AuthLayout } from '/src/layouts/auth/layout';
import { resetearPassword } from '/src/api/resetear-password'

const Page = () => {
  const [confirmation, setConfirmation] = useState(false);
  const searchParams = new URLSearchParams(document.location.search)
  const token = searchParams.get('token');
  const email = searchParams.get('email');

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
    onSubmit: async (values, helpers) => {
      const response = await resetearPassword(email, token, values.password);

      try {
        if (response.status == 200) {
          setConfirmation(true);
        }
        else if (response.status == 400) {
          helpers.setStatus({ success: false });
          helpers.setErrors(await response.json())
          helpers.setSubmitting(false);
        }
        else {
          throw new Error(response);
        }
      } catch (err) {

      }
    }
  });

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
