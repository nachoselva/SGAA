import { Box, Breadcrumbs, Container, Link, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useCallback, useEffect, useMemo, useState } from 'react';
import { getContratos } from '/src/api/administrador';
import { AuthGuard } from '/src/guards/auth-guard';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { ContratosSearch } from '/src/sections/contrato/contrato-search';
import { ContratosTable } from '/src/sections/contrato/contrato-table';
import { applyPagination } from '/src/utils/apply-pagination';
import { useRouter } from 'next/navigation';

const useContratos = (filteredData, page, rowsPerPage) => {
  return useMemo(
    () => {
      return applyPagination(filteredData, page, rowsPerPage);
    },
    [filteredData, page, rowsPerPage]
  );
};

const Page = () => {
  const router = useRouter();
  const [data, setData] = useState([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [searchText, setSearchText] = useState('');
  const lcSearchText = searchText.toLowerCase();
  const filteredData = data.filter((item) =>
    Object.values(item).some(
      field => field?.toString().toLowerCase().includes(lcSearchText)
    )
  );

  const contratos = useContratos(filteredData, page, rowsPerPage);

  const handleSearchChange = useCallback(
    (event) => {
      setSearchText(event.target.value);
      setPage(0);
    },
    []
  );

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

  useEffect(() => {
    getContratos()
      .then((response) => {
        setData(response);
      });
  }, []);

  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          SGAA - Contratos
        </title>
      </Head>
      <Box>
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Breadcrumbs aria-label="breadcrumb">

                <Link 
                  component="button"
                  underline="hover"
                  color="inherit"
                  onClick={() => router.push('/')} >
                  Inicio
                </Link>
                <Link
                  component="button"
                  underline="hover"
                  color="inherit"
                  onClick={() => router.push('/administrador/contrato')}
                >
                  Contratos
                </Link>
              </Breadcrumbs>
            </Stack>
          </Stack>
        </Container>
      </Box>
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
                  Contratos
                </Typography>
              </Stack>
            </Stack>
            <ContratosSearch onSearchChange={handleSearchChange} />
            <ContratosTable
              count={data.length}
              items={contratos}
              onPageChange={handlePageChange}
              onRowsPerPageChange={handleRowsPerPageChange}
              page={page}
              rowsPerPage={rowsPerPage}
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
