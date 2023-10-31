import { Link, TableRow, TableCell } from '@mui/material';
import { useRouter } from 'next/navigation';
import { getPostulaciones } from '/src/api/administrador';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

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
        {row.fechaPostulacion}
      </TableCell>
      <TableCell>
        {row.fechaOferta}
      </TableCell>
      <TableCell>
        {row.status}
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/aplicacion/' + row.aplicacionId)}>
          Ver Aplicacion
        </Link>
      </TableCell> <TableCell>
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
      { key: 'fechaPostulacion', title: 'Fecha Postulacion' },
      { key: 'fechaOferta', title: 'Fecha Oferta' },
      { key: 'status', title: 'Estado' },
      { key: null, title: null },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/', title: 'Inicio' },
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
