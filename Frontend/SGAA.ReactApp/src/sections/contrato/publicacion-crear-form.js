import { Box, Button, Stack, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { FancyDatePicker } from '/src/components/fancy-date-picker';

export const PublicacionCrearForm = (props) => {
  const { postulacion, handleSubmit, handleConfirmationChange } = props;

  const formik = useFormik({
    initialValues: {
      fechaDesde: null,
      fechaHasta: null,
      postulacionId: postulacion.id,
      submit: null
    },

    validationSchema: Yup.object({
      fechaDesde: Yup
        .max(255)
        .required('Fecha Desde es obligatorio'),
      fechaHasta: Yup
        .string()
        .required('Fecha Hasta es obligatorio')
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
            value={formik.values.fechaDesde}
            label="Fecha desde"
            name={"fechaDesde"}
            onChange={(value) => {
              formik.setFieldValue('fechaDesde', value && new Date(value));
            }}
            touched={formik.touched.fechaDesde}
            error={formik.errors.fechaDesde}
            onBlur={formik.handleBlur}
          />
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
          Enviar Contrato
        </Button>
      </form>
    </Box>
  );
};
