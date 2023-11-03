import { TableCell, TableRow } from '@mui/material';
import { getPublicaciones } from '/src/api/common';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const Page = () => {

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
    </TableRow >);

  const headerConfiguration =
    [
      { key: 'domicilioCompleto', title: 'Unidad' },
      { key: 'montoAlquiler', title: 'Monto Alquiler' },
      { key: 'inicioAlquiler', title: 'Disponible Desde' }
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
