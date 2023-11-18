import { Box, Button, Stack, Typography, TextField } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { FancyDatePicker } from '/src/components/fancy-date-picker';

export const ContratoCancelarForm = (props) => {
  const { contrato, handleSubmit, handleConfirmationChange } = props;

  const formik = useFormik({
    initialValues: {
      fechaCancelacion: '',
      contratoId: contrato.id,
      submit: null
    },

    validationSchema: Yup.object({
      fechaCancelacion: Yup
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
            value={formik.values.fechaCancelacion}
            label="Fecha de Cancelación"
            name={"fechaCancelacion"}
            onChange={(value) => {
              formik.setFieldValue('fechaCancelacion', value && new Date(value));
            }}
            touched={formik.touched.fechaCancelacion}
            error={formik.errors.fechaCancelacion}
            onBlur={formik.handleBlur}
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
            Cancelar Contrato
          </Button>
        </Stack>
      </form>
    </Box>
  );
};
