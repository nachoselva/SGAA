import { Box, Card, Container, Grid, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { FancyBreadcrumbs } from '/src/components/fancy-breadcrumbs';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const breadcrumbsConfig = [
  { url: '/inicio', title: 'Inicio' }
];

const Page = () => {
  return (
    <>
      <Head>
        <title>
          SGAA - Inicio
        </title>
      </Head>
      <FancyBreadcrumbs breadcrumbsConfig={breadcrumbsConfig}>
      </FancyBreadcrumbs>
      <Box
        component="main"
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
                  Inicio
                </Typography>
              </Stack>
            </Stack>
            <Grid
              container
              spacing={3}
            >
              <Grid
                xs={12}
              >
                <Card sx={{ p: 2 }} sx={{
                  border: 1, borderRadius: '8px', 'border- style': 'solid', 'border-width': '1px', 'border-color': '#1C2536', p: 2, mt: 1
                }} >
                  <p>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum a vestibulum leo. Quisque luctus interdum justo vel bibendum. Vestibulum eu dapibus urna. Suspendisse tortor lorem, scelerisque ac dignissim ac, euismod at ante. Quisque eu porttitor lorem. Maecenas lacinia, sem at ultrices scelerisque, enim urna fermentum ante, ac aliquet sapien eros vel augue. Nam accumsan tempus tincidunt. Suspendisse at commodo lectus, a fermentum neque. Praesent tristique, nulla ut suscipit ornare, libero purus sollicitudin ligula, sit amet aliquet justo lectus nec magna.
                    Praesent molestie interdum magna sit amet luctus. Fusce dapibus luctus lorem, sodales tristique ligula blandit ac. Nunc vel dolor a tortor vestibulum tempus. Donec euismod lectus a nulla rutrum, vitae iaculis ex sodales. Vestibulum imperdiet magna eget condimentum vulputate. Sed sit amet volutpat neque, id pellentesque neque. Maecenas et magna rhoncus, venenatis mauris sit amet, efficitur arcu. Nulla consectetur massa in tincidunt posuere. Vivamus interdum metus porta est cursus, et hendrerit est suscipit. Mauris leo enim, ullamcorper quis hendrerit aliquet, aliquam nec neque. Proin nunc elit, placerat id turpis quis, cursus lacinia lorem. Maecenas ultricies, risus sollicitudin mollis porttitor, nisl lorem feugiat velit, sed vulputate risus risus vitae dui. Vivamus lacus elit, semper at ante vel, vestibulum laoreet sem.
                    Quisque nec risus vitae velit pellentesque pharetra. Nullam vel metus massa. Nullam sodales, elit vitae pharetra elementum, ligula odio fermentum lectus, eu ullamcorper diam eros a arcu. Suspendisse eu ante nec diam egestas tincidunt in ac nibh. Aenean maximus suscipit libero ac ultrices. Duis dictum pharetra tincidunt. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae
                    Etiam tincidunt est sem, at tempus mauris tristique vitae. Proin vitae dui nec augue lobortis feugiat eget non lorem. Proin nisi leo, euismod id auctor in, vulputate quis urna. Vivamus eu ex interdum, auctor erat at, dictum ante. Donec eget viverra velit. Aenean eu tellus tempus, semper nunc nec, dictum nulla. Nulla magna urna, malesuada eu rutrum ut, consectetur vel tellus. Mauris vulputate neque enim, id scelerisque mauris ultrices eleifend. Donec vitae ipsum elementum risus mollis dignissim. Vestibulum vitae mi elit. Praesent feugiat, purus vel commodo varius, turpis lorem faucibus ex, aliquam tempus lorem dui nec enim. Aenean tempus elementum sollicitudin. Suspendisse potenti.
                    Ut nisi quam, imperdiet non sodales non, egestas sed lorem. Aliquam nec mi eu est eleifend vehicula ac eget sapien. Nullam interdum feugiat tincidunt. Proin ac pulvinar nunc. Quisque in justo viverra eros porta aliquet in id arcu. Etiam in efficitur mi. Phasellus efficitur at est ac auctor. Nullam interdum placerat sollicitudin. Morbi eu orci diam.
                  </p>
                </Card>
              </Grid>
            </Grid>
          </Stack>
        </Container>
      </Box>
    </>
  );
}



Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
