import { Box, Breadcrumbs, Container, Link, Stack } from '@mui/material';
import { useRouter } from 'next/navigation';

export const FancyBreadcrumbs = (props) =>
{
  const { breadcrumbsConfig } = props;
  const router = useRouter();

  return (<Box>
    <Container maxWidth="xl">
      <Stack spacing={3}>
        <Stack
          direction="row"
          justifyContent="space-between"
          spacing={4}
        >
          <Breadcrumbs aria-label="breadcrumb">
            {
              breadcrumbsConfig &&
              breadcrumbsConfig.map((row) =>
                <Link
                  component="button"
                  underline="hover"
                  color="inherit"
                  onClick={() => router.push(row.url)}>
                  {row.title}
                </Link>)
            }
          </Breadcrumbs>
        </Stack>
      </Stack>
    </Container>
  </Box>);
}