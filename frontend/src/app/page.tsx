'use client';

export default function Home () {
  function handleOnChange () {
    console.log('file added!');
  }

  return (
    <main>
      <input
        id="image"
        type="file"
        name="image"
        onChange={handleOnChange}
      />
    </main>
  );
}
 