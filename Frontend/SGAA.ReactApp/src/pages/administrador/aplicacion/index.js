import { Box, Container, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useCallback, useMemo, useState } from 'react';
import { AuthGuard } from '/src/guards/auth-guard';
import { useSelection } from '/src/hooks/use-selection';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { AplicacionesSearch } from '/src/sections/aplicacion/aplicacion-search';
import { AplicacionesTable } from '/src/sections/aplicacion/aplicacion-table';
import { applyPagination } from '/src/utils/apply-pagination';

const data = [
  {
    id: 2,
    status: 'AprobacionPendiente',
    inquilinoUsuarioNombreCompleto : 'nombre completo',
    postulaciones: 2,
    puntuacionTotal: 50
  }
];

const useAplicaciones = (page, rowsPerPage) => {
  return useMemo(
    () => {
      return applyPagination(data, page, rowsPerPage);
    },
    [page, rowsPerPage]
  );
};

const useAplicacionIds = (aplicaciones) => {
  return useMemo(
    () => {
      return aplicaciones.map((aplicacion) => aplicacion.id);
    },
    [aplicaciones]
  );
};

const Page = () => {
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const aplicaciones = useAplicaciones(page, rowsPerPage);
  const aplicacionesIds = useAplicacionIds(aplicaciones);
  const aplicacionesSelection = useSelection(aplicacionesIds);

  const handlePageChange = useCallback(
    (event, value) => {
      setPage(value);
    },
    []
  );

  const handleRowsPerPageChange = useCallback(
    (event) => {
      setRowsPerPage(event.target.value);
    },
    []
  );

  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          Aplicaciones
        </title>
      </Head>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          py: 8
        }}
      >
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Stack spacing={1}>
                <Typography variant="h4">
                  Aplicaciones
                </Typography>
              </Stack>
            </Stack>
            <AplicacionesSearch />
            <AplicacionesTable
              count={data.length}
              items={aplicaciones}
              onDeselectAll={aplicacionesSelection.handleDeselectAll}
              onDeselectOne={aplicacionesSelection.handleDeselectOne}
              onPageChange={handlePageChange}
              onRowsPerPageChange={handleRowsPerPageChange}
              onSelectAll={aplicacionesSelection.handleSelectAll}
              onSelectOne={aplicacionesSelection.handleSelectOne}
              page={page}
              rowsPerPage={rowsPerPage}
              selected={aplicacionesSelection.selected}
            />
          </Stack>
        </Container>
      </Box>
    </AuthGuard>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
