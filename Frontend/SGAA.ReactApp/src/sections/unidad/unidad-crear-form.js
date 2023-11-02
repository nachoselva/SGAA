import { Box, Button, Divider, Grid, MenuItem, TextField, Typography } from '@mui/material';
import { useFormik } from 'formik';
import React, { useEffect, useState } from 'react';
import * as Yup from 'yup';
import { getCiudades, getProvincias } from '/src/api/common';
import { FancyDatePicker } from '/src/components/fancy-date-picker';
import { FancyFilePicker } from '/src/components/fancy-file-picker';
import TrashIcon from '@heroicons/react/24/solid/TrashIcon';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';

export const UnidadCrearForm = (props) => {
  const formik = useFormik({
    initialValues: {
      provinciaId: -1,
      ciudadId: -1,
      calle: '',
      altura: null,
      piso: '',
      departamento: '',
      fechaAdquisicion: null,
      tituloPropiedadArchivo: null,
      detalle:
      {
        descripcion: '',
        superficie: '',
        ambientes: null,
        banios: null,
        dormitorios: null,
        cocheras: null,
        imagenes: []
      },
      titulares: [{
        nombre: '',
        apellido: '',
        email: '',
        numeroIdentificacion: '',
        domicilio: '',
        fechaNacimiento: null,
        frenteIdentificacionArchivo: null,
        dorsoIdentificacionArchivo: null
      }],
      submit: null
    },
    validationSchema: Yup.object({
      provinciaId: Yup
        .number()
        .positive('Provincia es obligatorio')
        .required('Provincia es obligatorio'),
      ciudadId: Yup
        .number()
        .positive('Ciudad es obligatorio')
        .required('Ciudad es obligatorio'),
      calle: Yup
        .string()
        .max(100, 'Hasta 100 caracteres')
        .required('Calle es obligatorio'),
      altura: Yup
        .string()
        .required('Altura es obligatorio'),
      piso: Yup
        .string()
        .max(25)
        .required('Piso es obligatorio'),
      departamento: Yup
        .string()
        .max(25)
        .required('Departamento es obligatorio'),
      fechaAdquisicion: Yup
        .string()
        .required('Fecha Adquisición es obligatorio'),
      tituloPropiedadArchivo: Yup
        .string()
        .required('Título de propiedad es obligatorio'),
      detalle: Yup.object({
        descripcion: Yup
          .string()
          .max(255)
          .required('Descripción es obligatorio'),
        superficie: Yup
          .string()
          .required('Superficie es obligatorio'),
        ambientes: Yup
          .string()
          .required('Ambientes es obligatorio'),
        banios: Yup
          .string()
          .required('Baños es obligatorio'),
        dormitorios: Yup
          .string()
          .required('Dormitorios es obligatorio'),
        cocheras: Yup
          .string()
          .required('Cocheras es obligatorio')
      }),
      titulares: Yup.array()
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
                .required('Título de propiedad es obligatorio'),
              dorsoIdentificacionArchivo: Yup
                .string()
                .required('Título de propiedad es obligatorio')
            })
        )
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

  const [provincias, setProvincias] = useState([]);
  const [ciudades, setCiudades] = useState([]);

  useEffect(() => {
    if (formik.values.provinciaId > 0)
      getCiudades(formik.values.provinciaId)
        .then((response) => {
          const orderedCiudades = response.sort((ciudad) => ciudad.nombre);
          formik.setFieldValue('ciudadId', -1, true);
          formik.setTouched({ 'ciudadId': false });
          setCiudades(orderedCiudades);
        });
  }, [formik.values.provinciaId]);

  useEffect(() => {
    formik.setTouched({});
    getProvincias()
      .then((response) => {
        const orderedProvincias = response.sort((provincia) => provincia.nombre);
        setProvincias(orderedProvincias);
      });
  }, []);

  const onTitularAdded = () => {
    const currentTitulares = formik.values.titulares.slice(0);

    currentTitulares.push({
      nombre: '',
      apellido: '',
      email: '',
      numeroIdentificacion: '',
      domicilio: '',
      fechaNacimiento: null,
      fechaNacimiento: null,
      frenteIdentificacionArchivo: null,
      dorsoIdentificacionArchivo: null
    });

    formik.setFieldValue('titulares', currentTitulares);
  }

  const onTitularRemoved = (index) => {
    const currentTitulares = formik.values.titulares.slice(0);
    formik.setFieldValue('titulares', currentTitulares.filter((_, i) => i !== index));
    formik.setTouched({ ...formik.touched, titulares: [] });
  }

  return (
    <Box>
      <form
        noValidate
        onSubmit={formik.handleSubmit}
      >
        <Grid container spacing={3} >
          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              select
              error={!!(formik.touched.provinciaId && formik.errors.provinciaId)}
              helperText={formik.touched.provinciaId && formik.errors.provinciaId}
              fullWidth
              onBlur={formik.handleBlur}
              name="provinciaId"
              value={formik.values.provinciaId}
              onChange={formik.handleChange}
              variant="filled"
              labelId="provinciaId-label"
              label={'Provincia'}
            >
              <MenuItem key={-1} value={-1}>
                --- Seleccione una provincia ---
              </MenuItem >
              {
                provincias && provincias.map(pro =>
                  <MenuItem key={pro.id} value={pro.id}>
                    {pro.nombre}
                  </MenuItem >
                )}
            </TextField>
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              select
              error={!!(formik.touched.ciudadId && formik.errors.ciudadId)}
              helperText={formik.touched.ciudadId && formik.errors.ciudadId}
              fullWidth
              onBlur={formik.handleBlur}
              name="ciudadId"
              value={formik.values.ciudadId}
              onChange={formik.handleChange}
              variant="filled"
              labelId="ciudadId-label"
              label={'Ciudad'}
            >
              <MenuItem key={-1} value={-1}>
                --- Seleccione una ciudad ---
              </MenuItem >
              {
                ciudades && ciudades.map(pro =>
                  <MenuItem key={pro.id} value={pro.id}>
                    {pro.nombre}
                  </MenuItem >
                )}
            </TextField>
          </Grid>
          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.calle && formik.errors.calle)}
              fullWidth
              helperText={formik.touched.calle && formik.errors.calle}
              label="Calle"
              name="calle"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.calle}
              InputProps={{
                inputProps: {
                  max: 100, min: 0
                }
              }}
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              type="number"
              InputProps={{
                inputProps: {
                  max: 10000, min: 0
                }
              }}
              error={!!(formik.touched.altura && formik.errors.altura)}
              fullWidth
              helperText={formik.touched.altura && formik.errors.altura}
              label="Altura"
              name="altura"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.altura}
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.piso && formik.errors.piso)}
              fullWidth
              helperText={formik.touched.piso && formik.errors.piso}
              label="Piso"
              name="piso"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.piso}
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.departamento && formik.errors.departamento)}
              fullWidth
              helperText={formik.touched.departamento && formik.errors.departamento}
              label="Departamento"
              name="departamento"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.departamento}
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <FancyDatePicker
              value={formik.values.fechaAdquisicion}
              label="Fecha Adquisición"
              name="fechaAdquisicion"
              onChange={(value) => {
                formik.setFieldValue('fechaAdquisicion', value && new Date(value));
              }}
              touched={formik.touched.fechaAdquisicion}
              error={formik.errors.fechaAdquisicion}
              onBlur={formik.handleBlur}
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <FancyFilePicker
              touched={formik.touched.tituloPropiedadArchivo}
              error={formik.errors.tituloPropiedadArchivo}
              label="Título de propiedad"
              name="tituloPropiedadArchivo"
              file={formik.values.tituloPropiedadArchivo}
              onBlur={() => {
                formik.setTouched({ ...formik.touched, tituloPropiedadArchivo: true });
              }}
              onChange={(result) => {
                formik.setFieldValue('tituloPropiedadArchivo', result);
              }}
            />
          </Grid>

          <Grid item xs={12}>
            <Typography variant="h5">
              Detalle
            </Typography>
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.detalle?.superficie && formik.errors.detalle?.superficie)}
              fullWidth
              helperText={formik.touched.detalle?.superficie && formik.errors.detalle?.superficie}
              label="Superficie"
              name="detalle.superficie"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.detalle.superficie}
              type="number"
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.detalle?.ambientes && formik.errors.detalle?.ambientes)}
              fullWidth
              helperText={formik.touched.detalle?.ambientes && formik.errors.detalle?.ambientes}
              label="Ambientes"
              name="detalle.ambientes"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.detalle.ambientes}
              type="number"
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.detalle?.banios && formik.errors.detalle?.banios)}
              fullWidth
              helperText={formik.touched.detalle?.banios && formik.errors.detalle?.banios}
              label="Baños"
              name="detalle.banios"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.detalle.banios}
              type="number"
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.detalle?.dormitorios && formik.errors.detalle?.dormitorios)}
              fullWidth
              helperText={formik.touched.detalle?.dormitorios && formik.errors.detalle?.dormitorios}
              label="Dormitorios"
              name="detalle.dormitorios"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.detalle.dormitorios}
              type="number"
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <TextField
              variant="filled"
              error={!!(formik.touched.detalle?.cocheras && formik.errors.detalle?.cocheras)}
              fullWidth
              helperText={formik.touched.detalle?.cocheras && formik.errors.detalle?.cocheras}
              label="Cocheras"
              name="detalle.cocheras"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.detalle.cocheras}
              type="number"
            />
          </Grid>

          <Grid item xs={12}>
            <TextField
              variant="filled"
              multiline
              rows={5}
              error={!!(formik.touched.detalle?.descripcion && formik.errors.detalle?.descripcion)}
              fullWidth
              helperText={formik.touched.detalle?.descripcion && formik.errors.detalle?.descripcion}
              label="Descripción"
              name="detalle.descripcion"
              onBlur={formik.handleBlur}
              onChange={formik.handleChange}
              value={formik.values.detalle.descripcion}
            />
          </Grid>
          <Grid item xs={12}>
            <Typography variant="h5">
              Titulares
            </Typography>
          </Grid>
          {
            formik.values.titulares.map((titular, index) =>
            (<Grid item xs={12}>
              <Box sx={{
                border: 1, borderRadius: '8px', 'border- style': 'solid', 'border-width': '1px', 'border-color': '#1C2536', p: 2, mt: 1
              }} >
                <Grid container spacing={3}>
                  {
                    formik.values.titulares.length > 1 &&
                    <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
                      <Button onClick={() => onTitularRemoved(index)} variant="contained">
                        <TrashIcon />
                      </Button>
                    </Grid>
                  }
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.titulares && formik.touched.titulares?.[index]?.nombre && formik.errors.titulares?.[index]?.nombre)}
                      fullWidth
                      helperText={formik.touched.titulares && formik.touched.titulares?.[index]?.nombre && formik.errors.titulares?.[index]?.nombre}
                      label="Nombre"
                      name={"titulares[" + index + "].nombre"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={titular.nombre}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.titulares && formik.touched.titulares?.[index]?.apellido && formik.errors.titulares?.[index]?.apellido)}
                      fullWidth
                      helperText={formik.touched.titulares && formik.touched.titulares?.[index]?.apellido && formik.errors.titulares?.[index]?.apellido}
                      label="Apellido"
                      name={"titulares[" + index + "].apellido"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={titular.apellido}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.titulares && formik.touched.titulares?.[index]?.email && formik.errors.titulares?.[index]?.email)}
                      fullWidth
                      helperText={formik.touched.titulares && formik.touched.titulares?.[index]?.email && formik.errors.titulares?.[index]?.email}
                      label="Email"
                      name={"titulares[" + index + "].email"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      type="email"
                      value={titular.email}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.titulares && formik.touched.titulares?.[index]?.numeroIdentificacion && formik.errors.titulares?.[index]?.numeroIdentificacion)}
                      fullWidth
                      helperText={formik.touched.titulares && formik.touched.titulares?.[index]?.numeroIdentificacion && formik.errors.titulares?.[index]?.numeroIdentificacion}
                      label="DNI"
                      name={"titulares[" + index + "].numeroIdentificacion"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={titular.numeroIdentificacion}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      variant="filled"
                      error={!!(formik.touched.titulares && formik.touched.titulares?.[index]?.domicilio && formik.errors.titulares?.[index]?.domicilio)}
                      fullWidth
                      helperText={formik.touched.titulares && formik.touched.titulares?.[index]?.domicilio && formik.errors.titulares?.[index]?.domicilio}
                      label="Domicilio"
                      name={"titulares[" + index + "].domicilio"}
                      onBlur={formik.handleBlur}
                      onChange={formik.handleChange}
                      value={titular.domicilio}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyDatePicker
                      value={titular.fechaNacimiento}
                      label="Fecha De Nacimiento"
                      name={"titulares[" + index + "].fechaNacimiento"}
                      onChange={(value) => {
                        const titulares = formik.values.titulares.slice(0);
                        titulares[index].fechaNacimiento = value && new Date(value);
                        console.log(titulares[index].fechaNacimiento);
                        formik.setFieldValue('titulares', titulares);
                      }}
                      touched={formik.touched.titulares?.[index]?.fechaNacimiento}
                      error={formik.errors.titulares?.[index]?.fechaNacimiento}
                      onBlur={formik.handleBlur}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      touched={formik.touched.titulares?.[index]?.frenteIdentificacionArchivo}
                      error={formik.errors.titulares?.[index]?.frenteIdentificacionArchivo}
                      label="Frente DNI"
                      name={"titulares[" + index + "].frenteIdentificacionArchivo"}
                      file={formik.values.titulares?.[index]?.frenteIdentificacionArchivo}
                      onBlur={() => {
                        const titulares = formik.touched.titulares ?? [];
                        const titular = formik.touched.titulares?.[index] ?? {};
                        titular.frenteIdentificacionArchivo = true;
                        titulares[index] = titular;
                        formik.setTouched({ ...formik.touched, titulares: titulares });
                      }}
                      onChange={(result) => {
                        formik.setFieldValue('titulares.[' + index + '].frenteIdentificacionArchivo', result);
                      }}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <FancyFilePicker
                      touched={formik.touched.titulares?.[index]?.dorsoIdentificacionArchivo}
                      error={formik.errors.titulares?.[index]?.dorsoIdentificacionArchivo}
                      label="Dorso DNI"
                      name={"titulares[" + index + "].dorsoIdentificacionArchivo"}
                      file={formik.values.titulares?.[index]?.dorsoIdentificacionArchivo}
                      onBlur={() => {
                        const titulares = formik.touched.titulares ?? [];
                        const titular = formik.touched.titulares?.[index] ?? {};
                        titular.dorsoIdentificacionArchivo = true;
                        titulares[index] = titular;
                        formik.setTouched({ ...formik.touched, titulares: titulares });
                      }}
                      onChange={(result) => {
                        formik.setFieldValue('titulares.[' + index + '].dorsoIdentificacionArchivo', result);
                      }}
                    />
                  </Grid>
                </Grid>
              </Box>
            </Grid>)
            )
          }
          <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
            <Button onClick={onTitularAdded} variant="contained">
              <PlusIcon></PlusIcon>
            </Button>
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
          Guardar
        </Button>
      </form>
    </Box >
  );
};
