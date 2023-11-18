import { Link, TableCell, TableRow } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import { postulacionStatus } from '/src/utils/status-labels';
import { aceptarOferta, cancelarPostulacion, getPostulaciones, rechazarOferta } from '/src/api/inquilino';
import { FancyDialog } from '/src/components/fancy-dialog';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const Page = () => {

  const router = useRouter();
  const [cancelarModalOpened, setCancelarModalOpened] = useState(false);
  const [aceptarModalOpened, setAceptarModalOpened] = useState(false);
  const [rechazarModalOpened, setRechazarModalOpened] = useState(false);
  const [modalIdOpened, setModalIdOpened] = useState(null);
  const [listChanged, setListChanged] = useState(null);

  useEffect(() => {
    if (listChanged)
      setListChanged(false);
  }, [listChanged]);

  const openCancelarModal = (id) => {
    setCancelarModalOpened(true);
    setModalIdOpened(id);
  }

  const anularCancelarAction = () => {
    setCancelarModalOpened(false);
    setModalIdOpened(null);
  }

  const confirmarCancelarAction = (id) => {
    cancelarPostulacion(id)
      .then(() => {
        setCancelarModalOpened(false);
        setModalIdOpened(null);
        setListChanged(true);
      })
  }

  const openAceptarModal = (id) => {
    setAceptarModalOpened(true);
    setModalIdOpened(id);
  }

  const anularAceptarAction = () => {
    setAceptarModalOpened(false);
    setModalIdOpened(null);
  }

  const confirmarAceptarAction = (id) => {
    aceptarOferta(id)
      .then(() => {
        setAceptarModalOpened(false);
        setModalIdOpened(null);
        setListChanged(true);
      })
  }

  const openRechazarModal = (id) => {
    setRechazarModalOpened(true);
    setModalIdOpened(id);
  }

  const anularRechazarAction = () => {
    setRechazarModalOpened(false);
    setModalIdOpened(null);
  }

  const confirmarRechazarAction = (id) => {
    rechazarOferta(id)
      .then(() => {
        setRechazarModalOpened(false);
        setModalIdOpened(null);
        setListChanged(true);
      });
  }

  const tableRowGenerator = (row) => (
    <TableRow
      hover
      key={row.id}
    >
      <TableCell>
        {row.id}
      </TableCell>
      <TableCell>
        {row.domicilioCompleto}
      </TableCell>
      <TableCell>
        {row.montoAlquiler}
      </TableCell>
      <TableCell>
        {postulacionStatus[row.status]}
      </TableCell>
      <TableCell>
        {row.fechaPostulacion && Moment(row.fechaPostulacion).format('DD/MM/yyyy hh:mm:ss')}
      </TableCell>
      <TableCell>
        {row.fechaOferta && Moment(row.fechaOferta).format('DD/MM/yyyy hh:mm:ss')}
      </TableCell>
      <TableCell>
        {

          row.status == 'Ofrecida'
          &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => openAceptarModal(row.id)}>
            Aceptar Oferta
          </Link>
        }
      </TableCell>
      <TableCell>
        {

          row.status == 'Ofrecida'
          &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => openRechazarModal(row.id)}>
            Rechazar Oferta
          </Link>
        }
      </TableCell>
      <TableCell>
        {

          row.status == 'Postulada'
          &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => openCancelarModal(row.id)}>
            Cancelar
          </Link>
        }
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/inquilino/postulacion/' + row.id)}>
          Ver Postulación
        </Link>
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/inquilino/aplicacion/' + row.aplicacionId)}>
          Ver Aplicación
        </Link>
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/inquilino/publicacion/' + row.publicacionId)}>
          Ver Publicación
        </Link>
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'domicilioCompleto', title: 'Unidad' },
      { key: 'montoAlquiler', title: 'Monto Alquiler' },
      { key: 'status', title: 'Estado' },
      { key: 'fechaPostulacion', title: 'Fecha Postulación' },
      { key: 'fechaOferta', title: 'Fecha Oferta' },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/inquilino/postulacion', title: 'Postulaciones' }
  ];

  return (
    <>
      <FancyDialog
        opened={cancelarModalOpened}
        actionName={"cancelar-postulacion"}
        param={modalIdOpened}
        title={"Cancelar Postulación"}
        content={"Esta acción cancelará su postulación de manera irreversible. ¿Desea continuar?"}
        options={[{ action: anularCancelarAction, text: 'Cancelar' }, { action: confirmarCancelarAction, text: 'Confirmar' }]} />
      <FancyDialog
        opened={aceptarModalOpened}
        actionName={"aceptar-oferta"}
        param={modalIdOpened}
        title={"Aceptar Oferta"}
        content={"Esta acción aceptará la oferta de manera irreversible. ¿Desea continuar?"}
        options={[{ action: anularAceptarAction, text: 'Cancelar' }, { action: confirmarAceptarAction, text: 'Confirmar' }]} />
      <FancyDialog
        opened={rechazarModalOpened}
        actionName={"rechazar-oferta"}
        param={modalIdOpened}
        title={"Rechazar Oferta"}
        content={"Esta acción rechazará la oferta de manera irreversible. ¿Desea continuar?"}
        options={[{ action: anularRechazarAction, text: 'Cancelar' }, { action: confirmarRechazarAction, text: 'Confirmar' }]} />
      <FancyTablePage
        getData={getPostulaciones}
        entityName={'Postulacion'}
        listName={'Postulaciones'}
        breadcrumbsConfig={breadcrumbsConfig}
        roles={['Inquilino']}
        headerConfiguration={headerConfiguration}
        tableRowGenerator={tableRowGenerator}
        refresh={listChanged}
      />
    </>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
