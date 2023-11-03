import { Link, TableRow, TableCell } from '@mui/material';
import { useRouter } from 'next/navigation';
import { getPublicaciones } from '/src/api/propietario';
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
        {row.inicioAlquiler}
      </TableCell>
      <TableCell>
        {row.postulaciones}
      </TableCell>
      <TableCell>
        {row.status}
      </TableCell>
      <TableCell>
        {
          row.status == 'Publicada' &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => { }}>
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
            onClick={() => { }}>
            Cancelar Publicación
          </Link>
        }
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/publicacion/' + row.id)}>
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
  { url: '/inquilino/aplicacion', title: 'Aplicaciones' }
];

return (
  <FancyTablePage
    getData={getPublicaciones}
    entityName={'Aplicación'}
    listName={'Aplicaciones'}
    breadcrumbsConfig={breadcrumbsConfig}
    roles={['Propietario']}
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
