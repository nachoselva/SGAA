import { Link, TableRow, TableCell } from '@mui/material';
import { useRouter } from 'next/navigation';
import { getUnidades } from '/src/api/administrador';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { unidadStatus } from '/src/utils/status-labels';

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
        {row.ciudad}
      </TableCell>
      <TableCell>
        {row.provincia}
      </TableCell>
      <TableCell>
        {unidadStatus[row.status]}
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/unidad/' + row.id)}>
          Ver Unidad
        </Link>
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'domicilioCompleto', title: 'Dirección' },
      { key: 'ciudad', title: 'Ciudad' },
      { key: 'provincia', title: 'Provincia' },
      { key: 'status', title: 'Estado' },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/administrador/unidad', title: 'Unidades' }
  ];

  return (
    <FancyTablePage
      getData={getUnidades}
      entityName={'Unidad'}
      listName={'Unidades'}
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
