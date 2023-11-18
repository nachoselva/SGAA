import { Box, Button, Grid, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { FancyDatePicker } from '/src/components/fancy-date-picker';

export const PagoCrearForm = (props) => {
  const formik = useFormik({
    initialValues: {
      contratoId: props.contratoId,
      descripcion: '',
      monto: '',
      fechaVencimiento: null,
      submit: null
    },
    validationSchema: Yup.object({
      descripcion: Yup
        .string()
        .max(100)
        .required('Descripción es obligatorio'),
      monto: Yup
        .string()
        .required('Monto es obligatorio'),
      fechaVencimiento: Yup
        .string()
        .required('Fecha de vencimiento es obligatorio')
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
        <Grid container spacing={3} >
          <Grid item xs={12}>
            <TextField
              variant="filled"
              error={!!(formik.touched.descripcion && formik.errors.descripcion)}
              multiline
              rows={5}
              fullWidth
              helperText={formik.touched.descripcion && formik.errors.descripcion}
              label="Descripción"
              name={"descripcion"}
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.descripcion}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              variant="filled"
              error={!!(formik.touched.monto && formik.errors.monto)}
              fullWidth
              helperText={formik.touched.monto && formik.errors.monto}
              label="Monto"
              name={"monto"}
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.monto}
              type="number"
            />
          </Grid>

          <Grid item xs={12}>
            <FancyDatePicker
              value={formik.values.fechaVencimiento}
              label="Fecha de Vencimiento"
              name={"fechaVencimiento"}
              onChange={(value) => {
                formik.setFieldValue('fechaVencimiento', value && new Date(value));
              }}
              touched={formik.touched.fechaVencimiento}
              error={formik.errors.fechaVencimiento}
              onBlur={formik.handleBlur}
            />
          </Grid>
        </Grid>
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
          Guadar
        </Button>
      </form>
    </Box >
  );
};
