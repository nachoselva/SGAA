import { Box, Button, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

export const UsuarioEditarForm = (props) => {

  console.log(props.rol);
  const formik = useFormik({
    initialValues: {
      nombre: props.nombre,
      apellido: props.apellido,
      submit: null
    },
    validationSchema: Yup.object({
      nombre: Yup
        .string()
        .max(255)
        .required('Nombre es obligatorio'),
      apellido: Yup
        .string()
        .max(255)
        .required('Apellido es obligatorio')
    }),
    onSubmit: (values, helpers) => {
      props.handleSubmit(values.nombre, values.apellido)
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
            fullWidth
            label="Email"
            name="email"
            type="email"
            value={props.email}
            InputProps={{
              readOnly: true,
            }}
            disabled
          />
          <TextField
            fullWidth
            label="Rol"
            name="rol"
            select
            SelectProps={{ native: true }}
            value={props.rol}
            InputProps={{
              readOnly: true,
            }}
            disabled
          >
            <option key={'Inquilino'} value={'Inquilino'}>Inquilino</option>
            <option key={'Propietario'} value={'Propietario'}>Propietario</option>
            <option key={'Administrador'} value={'Administrador'}>Administrador</option>
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
