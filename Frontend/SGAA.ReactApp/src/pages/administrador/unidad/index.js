import { Box, Container, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useCallback, useMemo, useState } from 'react';
import { useSelection } from '/src/hooks/use-selection';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UnidadesSearch } from '/src/sections/unidad/unidad-search';
import { UnidadesTable } from '/src/sections/unidad/unidad-table';
import { applyPagination } from '/src/utils/apply-pagination';
import { AuthGuard } from '/src/guards/auth-guard';

const data = [
  {
    id: 2,
    status: 'AprobacionPendiente',
    provincia : 'Santa Fé',
    ciudad: 'Rosario',
    domicilioCompleto: 'Corrientes 1500 P1 D2'
  }
];

const useUnidades = (page, rowsPerPage) => {
  return useMemo(
    () => {
      return applyPagination(data, page, rowsPerPage);
    },
    [page, rowsPerPage]
  );
};

const useUnidadIds = (unidades) => {
  return useMemo(
    () => {
      return unidades.map((unidad) => unidad.id);
    },
    [unidades]
  );
};

const Page = () => {
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const unidades = useUnidades(page, rowsPerPage);
  const unidadesIds = useUnidadIds(unidades);
  const unidadesSelection = useSelection(unidadesIds);

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
          Unidades
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
                  Unidades
                </Typography>
              </Stack>
            </Stack>
            <UnidadesSearch />
            <UnidadesTable
              count={data.length}
              items={unidades}
              onDeselectAll={unidadesSelection.handleDeselectAll}
              onDeselectOne={unidadesSelection.handleDeselectOne}
              onPageChange={handlePageChange}
              onRowsPerPageChange={handleRowsPerPageChange}
              onSelectAll={unidadesSelection.handleSelectAll}
              onSelectOne={unidadesSelection.handleSelectOne}
              page={page}
              rowsPerPage={rowsPerPage}
              selected={unidadesSelection.selected}
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
