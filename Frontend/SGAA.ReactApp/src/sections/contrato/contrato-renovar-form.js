import { Box, Button, Stack, Typography, TextField } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { FancyDatePicker } from '/src/components/fancy-date-picker';

export const ContratoRenovarForm = (props) => {
  const { contrato, handleSubmit, handleConfirmationChange } = props;

  const formik = useFormik({
    initialValues: {
      fechaHasta: '',
      montoAlquiler: '',
      contratoId: contrato.id,
      submit: null
    },

    validationSchema: Yup.object({
      fechaHasta: Yup
        .string()
        .required('Fecha Hasta es obligatorio'),
      montoAlquiler: Yup
        .string()
        .required('Monto Alquiler es obligatorio')
    }),
    onSubmit: (values, helpers) => {
      handleSubmit(values)
        .then(() => {
          handleConfirmationChange(true);
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
          <FancyDatePicker
            value={formik.values.fechaHasta}
            label="Fecha Hasta"
            name={"fechaHasta"}
            onChange={(value) => {
              formik.setFieldValue('fechaHasta', value && new Date(value));
            }}
            touched={formik.touched.fechaHasta}
            error={formik.errors.fechaHasta}
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
            type="number"
          />
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
            Enviar Contrato
          </Button>
        </Stack>
      </form>
    </Box>
  );
};
