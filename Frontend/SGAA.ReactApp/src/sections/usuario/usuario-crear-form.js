import { Box, Button, Checkbox, FormControl, InputLabel, ListItemText, MenuItem, Select, Stack, TextField, Typography } from '@mui/material';
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
      roles: props.defaultRol ? [props.defaultRol] : [],
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
        .max(100)
        .required('Password es obligatorio'),
      roles: Yup.array().of(Yup.string().max(20))
        .min(1, "Roles es obligatorio")
        .required('Roles es obligatorio')
    }),
    onSubmit: (values, helpers) => {
      props.handleSubmit(values)
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

  const roles = [];
  roles.push('Inquilino');
  roles.push('Propietario');
  if (props.includeAdminRol)
    roles.push('Administrador');

  const handleChangeSelect = (event) => {
    const value = event.target.value;
    let roles = formik.values.roles;
    const indexOf = roles.indexOf(value);
    if (indexOf > -1) {
      roles = roles.splice(indexOf, 1);
    } else {
      roles = roles.push(value);
    }
    event.target.value = value;
    formik.handleChange(event);
  }

  return (
    <Box>
      <form
        noValidate
        onSubmit={formik.handleSubmit}
      >
        <Stack spacing={3}>
          <TextField
            variant="filled"
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
            variant="filled"
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

          <FormControl
            variant="filled"
            margin={"1"}
            style={{ width: "100%", marginBottom: 32 }}
          >
            <InputLabel id="roles-label">Roles</InputLabel>
            <Select
              variant="filled"
              error={!!(formik.touched.roles && formik.errors.roles)}
              helperText={formik.touched.roles && formik.errors.roles}
              fullWidth
              onBlur={formik.handleBlur}
              name="roles"
              multiple
              value={formik.values.roles}
              onChange={handleChangeSelect}
              renderValue={(selected) => selected.join(', ')}
              labelId="roles-label"
              label={"Roles"}
            >
              {roles.map((rol) => (
                <MenuItem key={rol} value={rol}>
                  <Checkbox checked={formik.values.roles.indexOf(rol) > -1} />
                  <ListItemText primary={rol} />
                </MenuItem>
              ))}
            </Select>
          </FormControl>
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
