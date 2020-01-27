import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';
import { Player } from '../shared/player';
import { GamesDataSource } from '../shared/dataSources/gamesDataSource';
import { MatPaginator, MatSort } from '@angular/material';
import { GameService } from '../shared/services/game.service';
import { Matchday } from '../shared/matchday';
import { Game } from '../shared/game';
import { merge } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { PlayerService } from '../shared/services/player.service';

@Component({
  selector: 'app-game-table',
  templateUrl: './game-table.component.html',
  providers: [GameService],
  styleUrls: ['./game-table.component.css']
})
export class GameTableComponent implements OnInit, AfterViewInit {
  @Input() player?: Player;

  totalGames: number;
  isPlayerGames: boolean = this.route.snapshot.url[0].path === 'players';
  id: string = this.route.snapshot.params.id;
  isMatchdayGames: boolean = this.route.snapshot.url[0].path === 'matchdays';
  dataSource: GamesDataSource;
  displayedColumns: string[] = ['date', 'playerOne', 'playerOnePoints', 'playerTwoPoints', 'playerTwo'];

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private gameService: GameService, private route: ActivatedRoute) { }

  ngOnInit() {
    console.log(this.route);
    this.setTotalGames();
    this.dataSource = new GamesDataSource(this.gameService);

    if (this.isPlayerGames) {
      this.dataSource.loadPlayerGames(this.id, 'date', false, 10, 0);
      console.log(this.dataSource);
    } else if (this.isMatchdayGames) {
      this.dataSource.loadMatchdayGames(this.id, 'date', false, 10, 0);
    }
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadGamesPage())
      )
      .subscribe();
  }

  loadGamesPage() {
    if (this.isPlayerGames) {
      this.dataSource.loadPlayerGames(
        this.id,
        this.sort.active,
        (this.sort.direction === 'desc'),
        this.paginator.pageSize,
        this.paginator.pageIndex
      );
    } else if (this.isMatchdayGames) {
      this.dataSource.loadMatchdayGames(
        this.id,
        this.sort.active,
        (this.sort.direction === 'desc'),
        this.paginator.pageSize,
        this.paginator.pageIndex
      );
    }
  }

  setTotalGames() {
    if (this.isPlayerGames) {
      this.gameService.getNumberOfPlayerGames(this.id)
        .subscribe(res => {
          console.log(res);
          this.totalGames = res.totalCount;
          console.log('totalgames ' + this.totalGames);
        });
    } else if (this.isMatchdayGames) {
      this.gameService.getNumberOfMatchdayGames(this.id)
        .subscribe(res => {
          this.totalGames = res.totalCount;
        });
    }
  }

  isWinner(game: Game): boolean {
    if (this.isMatchdayGames) {
      return false;
    }

    if (this.player === null) {
      return false;
    }

    if (this.player.id === game.playerOne.id) {
      return game.playerOnePoints >= game.playerOneRequiredPoints;
    }
    return game.playerTwoPoints >= game.playerTwoRequiredPoints;
  }

}