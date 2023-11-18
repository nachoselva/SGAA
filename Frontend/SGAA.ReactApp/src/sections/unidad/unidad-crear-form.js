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
    initialValues: props.unidad ?? {
      provinciaId: -1,
      ciudadId: -1,
      calle: '',
      altura: '',
      piso: '',
      departamento: '',
      fechaAdquisicion: '',
      tituloPropiedadArchivo: null,
      detalle:
      {
        descripcion: '',
        superficie: '',
        ambientes: '',
        banios: '',
        dormitorios: '',
        cocheras: '',
        imagenes: []
      },
      titulares: [{
        nombre: '',
        apellido: '',
        email: '',
        numeroIdentificacion: '',
        domicilio: '',
        fechaNacimiento: '',
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
          .required('Cocheras es obligatorio'),
        imagenes: Yup.array()
          .of(
            Yup.object().shape(
              {
                archivo: Yup
                  .string()
                  .required('Imagen es obligatorio'),
                titulo: Yup
                  .string()
                  .max(100)
                  .required('Título es obligatorio'),
                descripcion: Yup
                  .string()
                  .max(500)
                  .required('Descripción es obligatorio')
              })
          )
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
                .required('Título de propiedad es obligatorio'),
              dorsoIdentificacionArchivo: Yup
                .string()
                .required('Título de propiedad es obligatorio')
            })
        )
        .min(1, "Titulares es obligatorio")
        .required('Titulares es obligatorio')
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
          setCiudades(orderedCiudades);
        });
  }, [formik.values.provinciaId]);

  useEffect(() => {
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

  const onImagenAdded = () => {
    const currentDetalle = { ...formik.values.detalle };

    currentDetalle.imagenes.push({
      archivo: null,
      titulo: '',
      descripcion: ''
    });

    formik.setFieldValue('detalle', currentDetalle);
  }

  const onImagenRemoved = (index) => {
    const currentDetalle = { ...formik.values.detalle };
    currentDetalle.imagenes = currentDetalle.imagenes.filter((_, i) => i !== index);
    formik.setFieldValue('detalle', currentDetalle);
    const detalleTouched = { ...formik.touched.detalle };
    detalleTouched.imagenes = [];
    formik.setTouched({ ...formik.touched, detalle: detalleTouched });
  }

  return (
    <Box>
      <form
        noValidate
        onSubmit={formik.handleSubmit}
      >
        <Grid container spacing={3} >
          <Grid item xs={12} sm={6}>
            {
              ((provincias &&
                provincias.length > 0) ||
                !props.unidad) &&
              <TextField
                variant="filled"
                select
                error={!!(formik.touched.provinciaId && formik.errors.provinciaId)}
                helperText={formik.touched.provinciaId && formik.errors.provinciaId}
                fullWidth
                onBlur={formik.handleBlur}
                name="provinciaId"
                value={formik.values.provinciaId}
                onChange={(event) => {
                  formik.setFieldValue('ciudadId', -1);
                  formik.handleChange(event);
                }}
                variant="filled"
                label={'Provincia'}
              >
                <MenuItem key={-1} value={-1}>
                  --- Seleccione una provincia ---
                </MenuItem >
                {
                  provincias.map(pro =>
                  (<MenuItem key={pro.id} value={pro.id}>
                    {pro.nombre}
                  </MenuItem >)
                  )}
              </TextField>
            }
          </Grid>

          <Grid item xs={12} sm={6}>
            {
              ((ciudades &&
                ciudades.length > 0) ||
                !props.unidad) &&
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
                label={'Ciudad'}
              >
                <MenuItem key={-1} value={-1}>
                  --- Seleccione una ciudad ---
                </MenuItem >
                {
                  ciudades.map(pro =>
                  (<MenuItem key={pro.id} value={pro.id}>
                    {pro.nombre}
                  </MenuItem >)
                  )
                }
              </TextField>
            }
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
            (<Grid item xs={12} key={index}>
              <Box sx={{
                border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
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

          {
            props.licencia == 'ProyectoFinal' &&
            <>
              <Grid item xs={12}>
                <Typography variant="h5">
                  Imagenes
                </Typography>
              </Grid>
              {
                formik.values.detalle.imagenes.map((img, index) =>
                (<Grid item xs={12} key={index}>
                  <Box sx={{
                    border: 1, borderRadius: '8px', 'borderStyle': 'solid', 'borderWidth': '1px', 'borderColor': '#1C2536', p: 2, mt: 1
                  }} >
                    <Grid container spacing={3}>
                      <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
                        <Button onClick={() => onImagenRemoved(index)} variant="contained">
                          <TrashIcon />
                        </Button>
                      </Grid>
                      <Grid item xs={12} sm={6}>
                        <TextField
                          variant="filled"
                          error={!!(formik.touched.detalle?.imagenes && formik.touched.detalle?.imagenes?.[index]?.titulo && formik.errors.detalle?.imagenes?.[index]?.titulo)}
                          fullWidth
                          helperText={formik.touched.titulares && formik.touched.detalle?.imagenes?.[index]?.titulo && formik.errors.detalle?.imagenes?.[index]?.titulo}
                          label="Título"
                          name={"detalle.imagenes[" + index + "].titulo"}
                          onBlur={formik.handleBlur}
                          onChange={formik.handleChange}
                          value={img.titulo}
                        />
                      </Grid>
                      <Grid item xs={12} sm={6}>
                        <FancyFilePicker
                          touched={formik.touched.detalle?.imagenes?.[index]?.archivo}
                          error={formik.errors.detalle?.imagenes?.[index]?.archivo}
                          label="Imagen"
                          name={"detalle.imagenes[" + index + "].archivo"}
                          file={formik.values.detalle?.imagenes?.[index]?.archivo}
                          onBlur={() => {
                            const detalle = formik.touched.detalle ?? {};
                            const imagenes = detalle.imagenes ?? [];
                            const imagen = imagenes[index] ?? {};
                            imagen.archivo = true;
                            imagenes[index] = imagen;
                            detalle.imagenes = imagenes;
                            formik.setTouched({ ...formik.touched, detalle: detalle });
                          }}
                          onChange={(result) => {
                            formik.setFieldValue('detalle.imagenes.[' + index + '].archivo', result);
                          }}
                        />
                      </Grid>
                      <Grid item xs={12}>
                        <TextField
                          multiline
                          rows={5}
                          variant="filled"
                          error={!!(formik.touched.detalle?.imagenes && formik.touched.detalle?.imagenes?.[index]?.descripcion && formik.errors.detalle?.imagenes?.[index]?.descripcion)}
                          fullWidth
                          helperText={formik.touched.titulares && formik.touched.detalle?.imagenes?.[index]?.descripcion && formik.errors.detalle?.imagenes?.[index]?.descripcion}
                          label="Descripción"
                          name={"detalle.imagenes[" + index + "].descripcion"}
                          onBlur={formik.handleBlur}
                          onChange={formik.handleChange}
                          value={img.descripcion}
                        />
                      </Grid>
                    </Grid>
                  </Box>
                </Grid>)
                )
              }
              <Grid item xs={12} sx={{ display: 'flex', flexDirection: 'row-reverse' }}>
                <Button onClick={onImagenAdded} variant="contained">
                  <PlusIcon></PlusIcon>
                </Button>
              </Grid>
            </>
          }
          {
            props.unidad?.comentarios?.length > 0 &&
            <Grid item xs={12}>
              <Typography variant="h5">
                Comentarios
              </Typography>
            </Grid>
          }
          {
            props.unidad?.comentarios?.length > 0 &&
            <Grid item xs={12}>
              <ul>
                {props.unidad.comentarios.sort((comentario) => comentario.fecha)
                  .map((com, index) => <li key={index}>[{com.fecha}] : {com.comentario}</li>)}
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
