import { Box, Button, Grid, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { FancyFilePicker } from '/src/components/fancy-file-picker';

export const PagoEditarForm = (props) => {
  const formik = useFormik({
    initialValues: {
      id: props.pago.id,
      archivo: null,
      submit: null
    },
    validationSchema: Yup.object({
      archivo: Yup
        .string()
        .required('Comprobante de pago es obligatorio')
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
            <FancyFilePicker
              touched={formik.touched.archivo}
              error={formik.errors.archivo}
              label="Comprobante de pago"
              name="archivo"
              file={formik.values.archivo}
              onBlur={() => {
                formik.setTouched({ ...formik.touched, archivo: true });
              }}
              onChange={(result) => {
                formik.setFieldValue('archivo', result);
              }}
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
          Abonar
        </Button>
      </form>
    </Box >
  );
};
