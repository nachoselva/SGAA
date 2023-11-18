import { Link, TableCell, TableRow } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/router';
import React from 'react';
import { getPagosByContrato } from '/src/api/inquilino';
import { FancyDownloadButton } from '/src/components/fancy-download-button';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { pagoStatus } from '/src/utils/status-labels';



const Page = () => {
  const router = useRouter();
  const contratoId = router.query.contratoId;

  const tableRowGenerator = (row) => (
    <TableRow
      hover
      key={row.id}
    >
      <TableCell>
        {row.id}
      </TableCell>
      <TableCell>
        {row.descripcion}
      </TableCell>
      <TableCell>
        {row.monto}
      </TableCell>
      <TableCell>
        {row.fechaVencimiento && Moment(row.fechaVencimiento).format('DD/MM/yyyy')}
      </TableCell>
      <TableCell>
        {pagoStatus[row.status]}
      </TableCell>
      <TableCell>
        {row.fechaPago && Moment(row.fechaPago).format('DD/MM/yyyy')}
      </TableCell>
      <TableCell>
        {row.domicilio}
      </TableCell>
      <TableCell>
        {row.archivo &&
          <FancyDownloadButton currentFile={JSON.parse(row.archivo)} />
        }
      </TableCell>
      <TableCell>
        {
          row.status != 'Pendiente' &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => router.push('/inquilino/contrato/' + contratoId + '/pago/' + row.id)}>
            Ver Pago
          </Link>
        }{
          row.status == 'Pendiente' &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => router.push('/inquilino/contrato/' + contratoId + '/pago/' + row.id+'/editar')}>
            Editar Pago
          </Link>
        }
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'descripcion', title: 'Descripción' },
      { key: 'monto', title: 'Monto' },
      { key: 'fechaVencimiento', title: 'Fecha de Vencimiento' },
      { key: 'pagoStatus', title: 'Estado' },
      { key: 'fechaPago', title: 'Fecha de Pago' },
      { key: 'domicilio', title: 'Domicilio' },
      { key: 'archivo', title: 'Comprobante de Pago' },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/inquilino/contrato', title: 'Contratos' },
    { url: '/inquilino/contrato/' + contratoId, title: contratoId },
    { url: '/inquilino/contrato/' + contratoId + '/pago', title: 'Pagos' }
  ];

  return (
    <>
      {contratoId &&
        <FancyTablePage
        getData={() => getPagosByContrato(contratoId)}
        entityName={'Pago'}
        listName={'Pagos'}
        breadcrumbsConfig={breadcrumbsConfig}
        roles={['Inquilino']}
        headerConfiguration={headerConfiguration}
        tableRowGenerator={tableRowGenerator}
        />}
    </>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
