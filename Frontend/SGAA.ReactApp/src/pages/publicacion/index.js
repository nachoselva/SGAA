import { Link, TableCell, TableRow } from '@mui/material';
import { getPublicaciones } from '/src/api/common';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { useRouter } from 'next/navigation';

const Page = () => {

  const router = useRouter();

  const tableRowGenerator = (row) => (
    <TableRow
      hover
      key={row.id}
    >
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
        {
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => router.push('/publicacion/' + row.codigo)}>
            Ver Publicación
          </Link>
        }
      </TableCell>
    </TableRow >);

  const headerConfiguration =
    [
      { key: 'domicilioCompleto', title: 'Unidad' },
      { key: 'montoAlquiler', title: 'Monto Alquiler' },
      { key: 'inicioAlquiler', title: 'Disponible Desde' },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/publicacion', title: 'Publicaciones' }
  ];

  return (
    <FancyTablePage
      getData={getPublicaciones}
      entityName={'Aplicación'}
      listName={'Aplicaciones'}
      breadcrumbsConfig={breadcrumbsConfig}
      headerConfiguration={headerConfiguration}
      tableRowGenerator={tableRowGenerator}
      allowAnnonymous={true}
    />
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
