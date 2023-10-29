import { Box, Button, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

export const UsuarioCrearForm = (props) => {
  const formik = useFormik({
    initialValues: {
      email: '',
      nombre: '',
      apellido: '',
      password: '',
      rol: props.defaultRol ? props.defaultRol : 'Inquilino',
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
      props.handleSubmit(values.email, values.nombre, values.apellido, values.password, values.rol)
        .then(() => {
          props.handleConfirmationChange(true);
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
    <Box>
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
            label="Rol"
            name="rol"
            onChange={formik.handleChange}
            select
            SelectProps={{ native: true }}
            value={formik.values.rol}
          >
            <option key={'Inquilino'} value={'Inquilino'}>Inquilino</option>
            <option key={'Propietario'} value={'Propietario'}>Propietario</option>
            {
              props.includeAdminRol &&
              <option key={'Administrador'} value={'Administrador'}>Administrador</option>
            }
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
          Guardar
        </Button>
      </form>
    </Box>
  );
};
