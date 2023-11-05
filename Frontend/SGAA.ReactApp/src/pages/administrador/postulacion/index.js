import { Link, TableCell, TableRow } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/navigation';
import { getPostulaciones } from '/src/api/administrador';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { postulacionStatus } from '/src/utils/status-labels';

const Page = () => {

  const router = useRouter();

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
        {row.postulantes}
      </TableCell>
      <TableCell>
        {row.fechaPostulacion && Moment(row.fechaPostulacion).format('DD/MM/yyyy hh:mm:ss')}
      </TableCell>
      <TableCell>
        {row.fechaOferta && Moment(row.fechaOferta).format('DD/MM/yyyy hh:mm:ss')}
      </TableCell>
      <TableCell>
        {postulacionStatus[row.status]}
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/postulacion/' + row.id)}>
          Ver Postulación
        </Link>
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/aplicacion/' + row.aplicacionId)}>
          Ver Aplicación
        </Link>
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/publicacion/' + row.publicacionId)}>
          Ver Publicación
        </Link>
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'domicilioCompleto', title: 'Unidad' },
      { key: 'montoAlquiler', title: 'Monto Alquiler' },
      { key: 'postulantes', title: 'Postulantes' },
      { key: 'fechaPostulacion', title: 'Fecha Postulación' },
      { key: 'fechaOferta', title: 'Fecha Oferta' },
      { key: 'status', title: 'Estado' },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/administrador/postulacion', title: 'Postulaciones' }
  ];

  return (
    <FancyTablePage
      getData={getPostulaciones}
      entityName={'Postulacion'}
      listName={'Postulaciones'}
      breadcrumbsConfig={breadcrumbsConfig}
      roles={['Administrador']}
      headerConfiguration={headerConfiguration}
      tableRowGenerator={tableRowGenerator}
    />
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
