import { Link, TableCell, TableRow } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import { cancelarPublicacion, cerrarPublicacion, getPublicaciones } from '/src/api/propietario';
import { FancyDialog } from '/src/components/fancy-dialog';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { publicacionStatus } from '/src/utils/status-labels';

const Page = () => {

  const router = useRouter();
  const [cancelarModalOpened, setCancelarModalOpened] = useState(false);
  const [cerrarModalOpened, setCerrarModalOpened] = useState(false);
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
    cancelarPublicacion(id);
    setCancelarModalOpened(false);
    setModalIdOpened(null);
    setListChanged(true);
  }

  const openCerrarModal = (id) => {
    setCerrarModalOpened(true);
    setModalIdOpened(id);
  }

  const anularCerrarAction = () => {
    setCerrarModalOpened(false);
    setModalIdOpened(null);
  }

  const confirmarCerrarAction = (id) => {
    cerrarPublicacion(id);
    setCerrarModalOpened(false);
    setModalIdOpened(null);
    setListChanged(true);
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
        {row.inicioAlquiler && Moment(row.inicioAlquiler).format('DD/MM/yyyy')}
      </TableCell>
      <TableCell>
        {row.postulaciones}
      </TableCell>
      <TableCell>
        {publicacionStatus[row.status]}
      </TableCell>
      <TableCell>
        {
          row.status == 'Publicada' &&
          row.postulaciones > 0 &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => openCerrarModal(row.id)}>
            Cerrar Publicación
          </Link>
        }
      </TableCell>
      <TableCell>
        {
          row.status == 'Publicada' &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => openCancelarModal(row.id)}>
            Cancelar Publicación
          </Link>
        }
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/propietario/publicacion/' + row.id)}>
          Ver Publicación
        </Link>
      </TableCell>
    </TableRow >);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'domicilioCompleto', title: 'Unidad' },
      { key: 'montoAlquiler', title: 'Monto Alquiler' },
      { key: 'inicioAlquiler', title: 'Disponible Desde' },
      { key: 'postulaciones', title: 'Cantidad Postulaciones' },
      { key: 'status', title: 'Estado' },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/propietario/publicacion', title: 'Publicaciones' }
  ];

  return (
    <>
      <FancyDialog
        opened={cancelarModalOpened}
        actionName={"cancelar-publicación"}
        param={modalIdOpened}
        title={"Cancelar Publicación"}
        content={"Esta acción cancelará su publicación de manera irreversible. ¿Desea continuar?"}
        options={[{ action: anularCancelarAction, text: 'Cancelar' }, { action: confirmarCancelarAction, text: 'Confirmar' }]} />
      <FancyDialog
        opened={cerrarModalOpened}
        actionName={"cerrar-publicación"}
        param={modalIdOpened}
        title={"Cerrar Publicación"}
        content={"Esta acción cerrará su publicación de manera irreversible y ofertará la unidad a un inquilino ¿Desea continuar?"}
        options={[{ action: anularCerrarAction, text: 'Cancelar' }, { action: confirmarCerrarAction, text: 'Confirmar' }]} />
      <FancyTablePage
        getData={getPublicaciones}
        entityName={'Publicación'}
        listName={'Publicaciones'}
        breadcrumbsConfig={breadcrumbsConfig}
        roles={['Propietario']}
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
