import { Link, TableRow, TableCell } from '@mui/material';
import { useRouter } from 'next/navigation';
import { getUsuarios } from '/src/api/administrador';
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
        {row.nombre}
      </TableCell>
      <TableCell>
        {row.apellido}
      </TableCell>
      <TableCell>
        {row.email}
      </TableCell>
      <TableCell>
        {row.roles}
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/usuario/' + row.id)}>
          Ver Usuario
        </Link>
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'nombre', title: 'Nombre' },
      { key: 'apellido', title: 'Apellido' },
      { key: 'email', title: 'Email' },
      { key: 'roles', title: 'Rol' },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/administrador/usuario', title: 'Usuarios' }
  ];

  return (
    <FancyTablePage
      getData={getUsuarios}
      entityName={'Usuario'}
      listName={'Usuarios'}
      breadcrumbsConfig={breadcrumbsConfig}
      roles={['Administrador']}
      onAddEntityRedirectTo={'/administrador/usuario/crear'}
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
