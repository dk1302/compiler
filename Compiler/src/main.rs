use std::fs::File;
use std::io::BufReader;
use std::io::prelude::*;
mod lex;

fn main() -> std::io::Result<()> {
    let file = File::open("src/foo.txt")?;
    let mut buf_reader = BufReader::new(file);
    let mut contents = String::new();
    buf_reader.read_to_string(&mut contents)?;
    let tokens = lex::lex(contents);
    Ok(())
}
