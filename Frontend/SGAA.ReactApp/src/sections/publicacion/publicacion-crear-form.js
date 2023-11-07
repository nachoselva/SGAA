import { Box, Button, Stack, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { FancyDatePicker } from '/src/components/fancy-date-picker';

export const PublicacionCrearForm = (props) => {
  const { unidad } = props;

  const formik = useFormik({
    initialValues: {
      inicioAlquiler: null,
      montoAlquiler: null,
      unidadId: props.unidad.id,
      submit: null
    },
    validationSchema: Yup.object({
      inicioAlquiler: Yup
        .string()
        .max(255)
        .required('Inicio Alquiler es obligatorio'),
      montoAlquiler: Yup
        .string()
        .max(255)
        .required('Monto Alquiler es obligatorio')
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

  return (
    <Box>
      <form
        noValidate
        onSubmit={formik.handleSubmit}
      >
        <Stack spacing={3}>
          <TextField
            variant="filled"
            fullWidth
            label="Domicilio"
            name="domicilio"
            type="domicilio"
            value={unidad.domicilioCompleto}
            InputProps={{
              readOnly: true,
            }}
            disabled
          />
          <FancyDatePicker
            value={formik.values.inicioAlquiler}
            label="Disponible desde"
            name={"inicioAlquiler"}
            onChange={(value) => {
              formik.setFieldValue('inicioAlquiler', value && new Date(value));
            }}
            touched={formik.touched.inicioAlquiler}
            error={formik.errors.inicioAlquiler}
            onBlur={formik.handleBlur}
          />
          <TextField
            variant="filled"
            error={!!(formik.touched.montoAlquiler && formik.errors.montoAlquiler)}
            fullWidth
            helperText={formik.touched.montoAlquiler && formik.errors.montoAlquiler}
            label="Monto Alquiler"
            name={"montoAlquiler"}
            onBlur={formik.handleBlur}
            onChange={formik.handleChange}
            value={formik.values.montoAlquiler}
            type='number'
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
          Guardar
        </Button>
      </form>
    </Box>
  );
};
