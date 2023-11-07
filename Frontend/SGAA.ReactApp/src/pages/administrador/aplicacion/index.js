import { Link, TableRow, TableCell } from '@mui/material';
import { useRouter } from 'next/navigation';
import { getAplicaciones } from '/src/api/administrador';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { aplicacionStatus } from '/src/utils/status-labels';

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
        {row.inquilinoUsuarioNombreCompleto}
      </TableCell>
      <TableCell>
        {aplicacionStatus[row.status]}
      </TableCell>
      <TableCell>
        {row.puntuacionTotal}
      </TableCell>
      <TableCell>
        {row.postulaciones}
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/aplicacion/' + row.id)}>
          Ver Aplicación
        </Link>
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'inquilinoUsuarioNombreCompleto', title: 'Inquilino' },
      { key: 'status', title: 'Estado' },
      { key: 'puntuacionTotal', title: 'Puntuación' },
      { key: 'postulaciones', title: 'Postulaciones' },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/administrador/aplicacion', title: 'Aplicaciones' }
  ];

  return (
    <FancyTablePage
      getData={getAplicaciones}
      entityName={'Aplicacion'}
      listName={'Aplicaciones'}
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
