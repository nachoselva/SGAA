import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import TrashIcon from '@heroicons/react/24/solid/TrashIcon';
import { Box, Button, Grid, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import { FancyDatePicker } from '/src/components/fancy-date-picker';
import { FancyFilePicker } from '/src/components/fancy-file-picker';

export const AplicacionCrearForm = (props) => {
  const formik = useFormik({
    initialValues: props.aplicacion ?? {
      postulantes: [{
        nombre: '',
        apellido: '',
        email: '',
        numeroIdentificacion: '',
        domicilio: '',
        fechaNacimiento: null,
        frenteIdentificacionArchivo: null,
        dorsoIdentificacionArchivo: null,
        fechaEmpleadoDesde: null,
        nombreEmpresa: '',
        ingresoMensual: null,
        reciboDeSueldoArchivo: null
      }],
      garantias: [{
        monto: null,
        archivo: null
      }],
      submit: null
    },
    validationSchema: Yup.object({
      postulantes: Yup.array()
        .of(
          Yup.object().shape(
            {
              nombre: Yup
                .string()
                .max(255)
                .required('Nombre es obligatorio'),
              apellido: Yup
                .string()
                .max(255)
                .required('Apellido es obligatorio'),
              email: Yup
                .string()
                .email('Formato de email inválido')
                .max(255)
                .required('Email es obligatorio'),
              fechaNacimiento: Yup
                .string()
                .required('Fecha de Nacimiento es obligatorio'),
              numeroIdentificacion: Yup
                .string()
                .max(255)
                .required('Dni es obligatorio'),
              domicilio: Yup
                .string()
                .max(255)
                .required('Domicilio es obligatorio'),
              frenteIdentificacionArchivo: Yup
                .string()
                .required('Frente DNI es obligatorio'),
              dorsoIdentificacionArchivo: Yup
                .string()
                .required('Dorso DNI es obligatorio'),
              fechaEmpleadoDesde: Yup
                .string()
                .required('Fecha Empleado es obligatorio'),
              nombreEmpresa: Yup
                .string()
                .required('Nombre Empresa es obligatorio'),
              ingresoMensual: Yup
                .string()
                .required('Ingreso mensual es obligatorio'),
              reciboDeSueldoArchivo: Yup
                .string()
                .required('Recibo de sueldo es obligatorio')
            })
        )
        .min(1, "Postulantes es obligatorio")
        .required('Postulantes es obligatorio'),

      garantias: Yup.array()
        .of(
          Yup.object().shape(
            {
              monto: Yup
                .string()
                .required('Monto es obligatorio'),
              archivo: Yup
                .string()
                .required('Archivo es obligatorio')
            })
        )
        .min(1, "Garantías es obligatorio")
        .required('Garantías es obligatorio')
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

  const onPostulanteAdded = () => {
    const currentPostulantes = formik.values.postulantes.slice(0);

    currentPostulantes.push({
      nombre: '',
      apellido: '',
      email: '',
      numeroIdentificacion: '',
      domicilio: '',
      fechaNacimiento: null,
      fechaNacimiento: null,
      frenteIdentificacionArchivo: null,
      dorsoIdentificacionArchivo: null,
      fechaEmpleadoDesde: null,
      nombreEmpresa: '',
      ingresoMensual: null,
      reciboDeSueldoArchivo: null
    });

    formik.setFieldValue('postulantes', currentPostulantes);
  }

  const onPostulanteRemoved = (index) => {
    const currentPostulantes = formik.values.postulantes.slice(0);
    formik.setFieldValue('postulantes', currentPostulantes.filter((_, i) => i !== index));
    formik.setTouched({ ...formik.touched, postulantes: [] });
  }

  const onGarantiaAdded = () => {
    const currentGarantias = formik.values.garantias.slice(0);

    currentGarantias.push({
      monto: null,
      archivo: null
    });

    formik.setFieldValue('garantias', currentGarantias);
  }

  const onGarantiaRemoved = (index) => {
    const currentGarantias = formik.values.garantias.slice(0);
    formik.setFieldValue('garantias', currentGarantias.filter((_, i) => i !== index));
    formik.setTouched({ ...formik.touched, garantias: [] });
  }

  return (
    <Box>
      <form
        noValidate
        onSubmit={formik.handleSubmit}
      >
        <Grid container spacing={3} >
          <Grid item xs={12}>
            <Typography variant="h5">
              Postulantes
            </Typography>
          </Grid>
          {
            formik.values.postulantes.map((postulante, index) =>
            (<Grid item xs={12} key={index}>
              <Box sx={{
                border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
              }} >
                <Grid container spacing={3}>
                  {
                    formik.values.postulantes.length > 1 &&
                    <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
                      <Button onClick={() => onPostulanteRemoved(index)} variant="contained">
                        <TrashIcon />
                      </Button>
                    </Grid>
                  }
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.postulantes && formik.touched.postulantes?.[index]?.nombre && formik.errors.postulantes?.[index]?.nombre)}
                      fullWidth
                      helperText={formik.touched.postulantes && formik.touched.postulantes?.[index]?.nombre && formik.errors.postulantes?.[index]?.nombre}
                      label="Nombre"
                      name={"postulantes[" + index + "].nombre"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={postulante.nombre}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.postulantes && formik.touched.postulantes?.[index]?.apellido && formik.errors.postulantes?.[index]?.apellido)}
                      fullWidth
                      helperText={formik.touched.postulantes && formik.touched.postulantes?.[index]?.apellido && formik.errors.postulantes?.[index]?.apellido}
                      label="Apellido"
                      name={"postulantes[" + index + "].apellido"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={postulante.apellido}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.postulantes && formik.touched.postulantes?.[index]?.email && formik.errors.postulantes?.[index]?.email)}
                      fullWidth
                      helperText={formik.touched.postulantes && formik.touched.postulantes?.[index]?.email && formik.errors.postulantes?.[index]?.email}
                      label="Email"
                      name={"postulantes[" + index + "].email"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      type="email"
                      value={postulante.email}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.postulantes && formik.touched.postulantes?.[index]?.numeroIdentificacion && formik.errors.postulantes?.[index]?.numeroIdentificacion)}
                      fullWidth
                      helperText={formik.touched.postulantes && formik.touched.postulantes?.[index]?.numeroIdentificacion && formik.errors.postulantes?.[index]?.numeroIdentificacion}
                      label="DNI"
                      name={"postulantes[" + index + "].numeroIdentificacion"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={postulante.numeroIdentificacion}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.postulantes && formik.touched.postulantes?.[index]?.domicilio && formik.errors.postulantes?.[index]?.domicilio)}
                      fullWidth
                      helperText={formik.touched.postulantes && formik.touched.postulantes?.[index]?.domicilio && formik.errors.postulantes?.[index]?.domicilio}
                      label="Domicilio"
                      name={"postulantes[" + index + "].domicilio"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={postulante.domicilio}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyDatePicker
                      value={postulante.fechaNacimiento}
                      label="Fecha De Nacimiento"
                      name={"postulantes[" + index + "].fechaNacimiento"}
                      onChange={(value) => {
                        const postulantes = formik.values.postulantes.slice(0);
                        postulantes[index].fechaNacimiento = value && new Date(value);
                        formik.setFieldValue('postulantes', postulantes);
                      }}
                      touched={formik.touched.postulantes?.[index]?.fechaNacimiento}
                      error={formik.errors.postulantes?.[index]?.fechaNacimiento}
                      onBlur={formik.handleBlur}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      touched={formik.touched.postulantes?.[index]?.frenteIdentificacionArchivo}
                      error={formik.errors.postulantes?.[index]?.frenteIdentificacionArchivo}
                      label="Frente DNI"
                      name={"postulantes[" + index + "].frenteIdentificacionArchivo"}
                      file={formik.values.postulantes?.[index]?.frenteIdentificacionArchivo}
                      onBlur={() => {
                        const postulantes = formik.touched.postulantes ?? [];
                        const postulante = formik.touched.postulantes?.[index] ?? {};
                        postulante.frenteIdentificacionArchivo = true;
                        postulantes[index] = postulante;
                        formik.setTouched({ ...formik.touched, postulantes: postulantes });
                      }}
                      onChange={(result) => {
                        formik.setFieldValue('postulantes.[' + index + '].frenteIdentificacionArchivo', result);
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      touched={formik.touched.postulantes?.[index]?.dorsoIdentificacionArchivo}
                      error={formik.errors.postulantes?.[index]?.dorsoIdentificacionArchivo}
                      label="Dorso DNI"
                      name={"postulantes[" + index + "].dorsoIdentificacionArchivo"}
                      file={formik.values.postulantes?.[index]?.dorsoIdentificacionArchivo}
                      onBlur={() => {
                        const postulantes = formik.touched.postulantes ?? [];
                        const postulante = formik.touched.postulantes?.[index] ?? {};
                        postulante.dorsoIdentificacionArchivo = true;
                        postulantes[index] = postulante;
                        formik.setTouched({ ...formik.touched, postulantes: postulantes });
                      }}
                      onChange={(result) => {
                        formik.setFieldValue('postulantes.[' + index + '].dorsoIdentificacionArchivo', result);
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.postulantes && formik.touched.postulantes?.[index]?.nombreEmpresa && formik.errors.postulantes?.[index]?.nombreEmpresa)}
                      fullWidth
                      helperText={formik.touched.postulantes && formik.touched.postulantes?.[index]?.nombreEmpresa && formik.errors.postulantes?.[index]?.nombreEmpresa}
                      label="Nombre Empresa"
                      name={"postulantes[" + index + "].nombreEmpresa"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={postulante.nombreEmpresa}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyDatePicker
                      value={postulante.fechaEmpleadoDesde}
                      label="Fecha de Empleo"
                      name={"postulantes[" + index + "].fechaEmpleadoDesde"}
                      onChange={(value) => {
                        const postulantes = formik.values.postulantes.slice(0);
                        postulantes[index].fechaEmpleadoDesde = value && new Date(value);
                        formik.setFieldValue('postulantes', postulantes);
                      }}
                      touched={formik.touched.postulantes?.[index]?.fechaEmpleadoDesde}
                      error={formik.errors.postulantes?.[index]?.fechaEmpleadoDesde}
                      onBlur={formik.handleBlur}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.postulantes && formik.touched.postulantes?.[index]?.ingresoMensual && formik.errors.postulantes?.[index]?.ingresoMensual)}
                      fullWidth
                      helperText={formik.touched.postulantes && formik.touched.postulantes?.[index]?.ingresoMensual && formik.errors.postulantes?.[index]?.ingresoMensual}
                      label="Ingreso Mensual"
                      name={"postulantes[" + index + "].ingresoMensual"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={postulante.ingresoMensual}
                      type="number"
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      touched={formik.touched.postulantes?.[index]?.reciboDeSueldoArchivo}
                      error={formik.errors.postulantes?.[index]?.reciboDeSueldoArchivo}
                      label="Recibo de sueldo"
                      name={"postulantes[" + index + "].reciboDeSueldoArchivo"}
                      file={formik.values.postulantes?.[index]?.reciboDeSueldoArchivo}
                      onBlur={() => {
                        const postulantes = formik.touched.postulantes ?? [];
                        const postulante = formik.touched.postulantes?.[index] ?? {};
                        postulante.reciboDeSueldoArchivo = true;
                        postulantes[index] = postulante;
                        formik.setTouched({ ...formik.touched, postulantes: postulantes });
                      }}
                      onChange={(result) => {
                        formik.setFieldValue('postulantes.[' + index + '].reciboDeSueldoArchivo', result);
                      }}
                    />
                  </Grid>
                </Grid>
              </Box>
            </Grid>)
            )
          }
          <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
            <Button onClick={onPostulanteAdded} variant="contained">
              <PlusIcon></PlusIcon>
            </Button>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="h5">
              Garantías
            </Typography>
          </Grid>
          {
            formik.values.garantias.map((postulante, index) =>
            (<Grid item xs={12} key={index}>
              <Box sx={{
                border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
              }} >
                <Grid container spacing={3}>
                  {
                    formik.values.garantias.length > 1 &&
                    <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
                      <Button onClick={() => onGarantiaRemoved(index)} variant="contained">
                        <TrashIcon />
                      </Button>
                    </Grid>
                  }
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.garantias && formik.touched.garantias?.[index]?.monto && formik.errors.garantias?.[index]?.monto)}
                      fullWidth
                      helperText={formik.touched.garantias && formik.touched.garantias?.[index]?.monto && formik.errors.garantias?.[index]?.monto}
                      label="Monto"
                      name={"garantias[" + index + "].monto"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={postulante.monto}
                      type="number"
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      touched={formik.touched.garantias?.[index]?.archivo}
                      error={formik.errors.garantias?.[index]?.archivo}
                      label="Recibo de sueldo"
                      name={"garantias[" + index + "].archivo"}
                      file={formik.values.garantias?.[index]?.archivo}
                      onBlur={() => {
                        const garantias = formik.touched.garantias ?? [];
                        const postulante = formik.touched.garantias?.[index] ?? {};
                        postulante.archivo = true;
                        garantias[index] = postulante;
                        formik.setTouched({ ...formik.touched, garantias: garantias });
                      }}
                      onChange={(result) => {
                        formik.setFieldValue('garantias.[' + index + '].archivo', result);
                      }}
                    />
                  </Grid>
                </Grid>
              </Box>
            </Grid>)
            )
          }
          <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
            <Button onClick={onGarantiaAdded} variant="contained">
              <PlusIcon></PlusIcon>
            </Button>
          </Grid>
          {
            props.aplicacion?.comentarios?.length > 0 &&
            <Grid item xs={12}>
              <Typography variant="h5">
                Comentarios
              </Typography>
            </Grid>
          }
          {
            props.aplicacion?.comentarios?.length > 0 &&
            <Grid item xs={12}>
              <ul>
                  {props.aplicacion.comentarios.sort((comentario) => comentario.fecha).map((com, index) =>
                    <li key={index}>[{com.fecha}] : {com.comentario}</li>
                )}
              </ul>
            </Grid>
          }
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
          Guardar
        </Button>
      </form>
    </Box >
  );
};
